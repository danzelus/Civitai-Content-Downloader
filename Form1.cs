using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


using CivitaiVideoDownloader.Properties;

namespace CivitaiVideoDownloader
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private CancellationTokenSource? cts;
        private int fixedWidth;
        private const string ConfigFile = "config.json";
        private AppConfig config = new AppConfig();

        public Form1()
        {
            InitializeComponent();
            stopButton.Enabled = false;

            sortComboBox.Items.AddRange(new string[] { "Newest", "Most Reactions", "Most Comments", "Most Collected" });
            periodComboBox.Items.AddRange(new string[] { "AllTime", "Year", "Month", "Week", "Day" });

            LoadConfig();
            ApplyConfigToUI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Lock initial width - commented out to allow resizing
            // fixedWidth = this.Width;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Keep width fixed - commented out to allow resizing
            // if (fixedWidth > 0)
            // {
            //     this.Width = fixedWidth;
            // }
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            string[] usernames = usernameBox.Lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Trim()).ToArray();
            string apiKey = apiKeyBox.Text.Trim();

            
            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("Enter API key!");
                return;
            }

            downloadButton.Enabled = false;
            stopButton.Enabled = true;
            statusLabel.Text = "Starting...";
            logBox.Clear();
            cts = new CancellationTokenSource();

            try
            {
                int totalLimit = (int)limitNumericUpDown.Value;
                if (totalLimit == 0)
                {
                    totalLimit = int.MaxValue;
                }

                int totalDownloadedInSession = 0;
                overallProgress.Value = 0;
                overallProgress.Maximum = totalLimit == int.MaxValue ? 100 : totalLimit;

                
                if (usernames.Length > 0)
                {
                    
                    for (int i = 0; i < usernames.Length; i++)
                    {
                        if (totalDownloadedInSession >= totalLimit && limitNumericUpDown.Value != 0)
                        {
                            Log("Global download limit reached for this session.");
                            break;
                        }
                        cts.Token.ThrowIfCancellationRequested();
                        string currentUser = usernames[i];
                        statusLabel.Text = $"Processing User: {currentUser} ({i + 1}/{usernames.Length})";
                        Log($"\n▶▶▶ Starting Download for User: {currentUser} ◀◀◀");
                        int remainingLimit = (limitNumericUpDown.Value == 0) ? int.MaxValue : totalLimit - totalDownloadedInSession;
                        int count = await DownloadContent(currentUser, apiKey, remainingLimit, cts.Token);
                        totalDownloadedInSession += count;
                    }
                }
                else
                {
                    
                    statusLabel.Text = "Starting global search by filters...";
                    Log($"\n▶▶▶ Starting global search by filters ◀◀◀");
                    int count = await DownloadContent(string.Empty, apiKey, totalLimit, cts.Token); // Передаём пустое имя
                    totalDownloadedInSession += count;
                }


                statusLabel.Text = $"All done! Total new files downloaded: {totalDownloadedInSession}.";
            }
            catch (OperationCanceledException)
            {
                statusLabel.Text = "Canceled!";
                Log("⚠ Download canceled by user");
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error!";
                MessageBox.Show($"An critical error occurred: {ex.Message}");
            }
            finally
            {
                downloadButton.Enabled = true;
                stopButton.Enabled = false;
                cts = null;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }

        private async Task<int> DownloadContent(string username, string apiKey, int limit, CancellationToken token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var urlParams = new StringBuilder();

            
            if (!string.IsNullOrEmpty(username))
            {
                urlParams.Append($"&username={WebUtility.UrlEncode(username)}");
            }

            if (nsfwCheck.Checked) urlParams.Append("&nsfw=true");
            var nsfwLevels = new List<string>();
            if (sfwCheck.Checked) nsfwLevels.Add("None");
            if (nsfwCheck.Checked) { nsfwLevels.Add("Soft"); nsfwLevels.Add("Mature"); nsfwLevels.Add("X"); }
            if (nsfwLevels.Count > 0) urlParams.Append($"&nsfwLevel={string.Join(",", nsfwLevels)}");

            string sort = sortComboBox.SelectedItem?.ToString() ?? "Newest";
            string period = periodComboBox.SelectedItem?.ToString() ?? "AllTime";
            urlParams.Append($"&sort={WebUtility.UrlEncode(sort)}");
            urlParams.Append($"&period={WebUtility.UrlEncode(period)}");

            int batchSize = Math.Min(limit, 100);
            string baseUrl = $"https://civitai.com/api/v1/images?limit={batchSize}{urlParams}";
            string? nextPage = baseUrl;
            int newlyDownloaded = 0;
            int processedForLimit = 0;

            
            string folderIdentifier;
            if (!string.IsNullOrEmpty(username))
            {
                folderIdentifier = username;
            }
            else
            {
                folderIdentifier = SanitizeFolderName($"{sort}_{period}_Global");
            }

            string baseOutputPath = SanitizeFolderName(outputFolderBox.Text);
            if (!string.IsNullOrEmpty(baseOutputPath)) Directory.CreateDirectory(baseOutputPath);

            string mainContentFolder = Path.Combine(baseOutputPath, folderIdentifier);
            Directory.CreateDirectory(mainContentFolder);
            string videosSubFolder = Path.Combine(mainContentFolder, "videos");
            string imagesSubFolder = Path.Combine(mainContentFolder, "images");

            try
            {
                string responseCheck = await ExecuteWithRetry(() => client.GetStringAsync(baseUrl, token), token);
                using JsonDocument docCheck = JsonDocument.Parse(responseCheck);
                if (docCheck.RootElement.TryGetProperty("metadata", out JsonElement meta) && meta.TryGetProperty("totalItems", out JsonElement ti))
                {
                    int totalFound = ti.GetInt32();
                    if (limitNumericUpDown.Value > 0)
                    {
                        overallProgress.Maximum = Math.Min(totalFound, limit);
                    }
                }
            }
            catch (OperationCanceledException) { throw; }
            catch { /* Ignore */ }

            while (!string.IsNullOrEmpty(nextPage))
            {
                if (processedForLimit >= limit) break;

                token.ThrowIfCancellationRequested();
                string response = await ExecuteWithRetry(() => client.GetStringAsync(nextPage, token), token);

                using JsonDocument doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                if (root.TryGetProperty("items", out JsonElement items))
                {
                    foreach (var item in items.EnumerateArray())
                    {
                        if (processedForLimit >= limit) break;
                        token.ThrowIfCancellationRequested();

                        string? url = item.GetProperty("url").GetString();
                        if (string.IsNullOrEmpty(url)) continue;

                        bool isVideo = IsVideo(url);

                        if ((isVideo && !downloadVideosCheck.Checked) || (!isVideo && !downloadImagesCheck.Checked))
                        {
                            continue;
                        }

                        processedForLimit++;

                        if (isVideo) Directory.CreateDirectory(videosSubFolder);
                        else Directory.CreateDirectory(imagesSubFolder);

                        string id = item.GetProperty("id").ToString();
                        string created = item.GetProperty("createdAt").GetString() ?? "file";
                        string safeName = created.Replace(":", "-").Replace("T", "_");
                        string ext = Path.GetExtension(url);

                        string author = item.TryGetProperty("username", out JsonElement un) ? un.GetString() ?? "unknown" : "unknown";
                        string fileName = $"{author}__{safeName}_{id}{ext}";

                        string destinationFolder = isVideo ? videosSubFolder : imagesSubFolder;
                        string path = Path.Combine(destinationFolder, fileName);

                        if (File.Exists(path))
                        {
                            Log($"Skipped (exists, counts to limit): {fileName}");
                            if (overallProgress.Value < overallProgress.Maximum) overallProgress.Value++;
                            continue;
                        }

                        Log($"⬇ Downloading: {fileName}");
                        await DownloadFileWithProgress(url, path, token);
                        newlyDownloaded++;
                        if (overallProgress.Value < overallProgress.Maximum) overallProgress.Value++;
                    }
                }

                if (processedForLimit >= limit)
                {
                    nextPage = null;
                }
                else if (root.TryGetProperty("metadata", out JsonElement meta2) && meta2.TryGetProperty("nextPage", out JsonElement np))
                {
                    nextPage = np.GetString();
                }
                else
                {
                    nextPage = null;
                }
            }
            return newlyDownloaded;
        }

        private async Task DownloadFileWithProgress(string url, string path, CancellationToken token)
        {
            var retryDelay = TimeSpan.FromSeconds(15);
            while (true)
            {
                token.ThrowIfCancellationRequested();
                try
                {
                    using var responseMsg = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);
                    responseMsg.EnsureSuccessStatusCode();
                    var totalBytes = responseMsg.Content.Headers.ContentLength ?? -1L;
                    using var stream = await responseMsg.Content.ReadAsStreamAsync(token);
                    using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
                    fileProgress.Value = 0;
                    if (totalBytes > 0) fileProgress.Maximum = 100;
                    var buffer = new byte[8192];
                    long totalRead = 0;
                    int read;
                    while ((read = await stream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                    {
                        await fs.WriteAsync(buffer.AsMemory(0, read), token);
                        totalRead += read;
                        if (totalBytes > 0)
                        {
                            int percent = (int)((totalRead * 100L) / totalBytes);
                            fileProgress.Value = Math.Min(percent, 100);
                        }
                    }
                    return;
                }
                catch (HttpRequestException ex)
                {
                    Log($"⚠ Network error downloading file: {ex.Message}. Retrying in {retryDelay.Seconds} seconds...");
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); }
                        catch (Exception deleteEx) { Log($"Could not delete partial file: {deleteEx.Message}"); }
                    }
                    await Task.Delay(retryDelay, token);
                }
            }
        }

        private async Task<T> ExecuteWithRetry<T>(Func<Task<T>> action, CancellationToken token)
        {
            var retryDelay = TimeSpan.FromSeconds(15);
            while (true)
            {
                token.ThrowIfCancellationRequested();
                try
                {
                    return await action();
                }
                catch (HttpRequestException ex)
                {
                    Log($"⚠ Network error: {ex.Message}. Retrying in {retryDelay.Seconds} seconds...");
                    await Task.Delay(retryDelay, token);
                }
            }
        }

        private string SanitizeFolderName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            foreach (char c in invalidChars) { name = name.Replace(c.ToString(), ""); }
            return name.Trim();
        }

        private bool IsVideo(string url)
        {
            string lowerUrl = url.ToLower();
            string extension = Path.GetExtension(lowerUrl);
            return extension == ".mp4" || extension == ".webm" || extension == ".mov";
        }

        private void Log(string msg)
        {
            if (InvokeRequired) { Invoke(new Action<string>(Log), msg); return; }
            logBox.AppendText(msg + Environment.NewLine);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => SaveConfig();

        private void LoadConfig()
        {
            if (!File.Exists(ConfigFile)) return;
            try { config = JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(ConfigFile)) ?? new AppConfig(); }
            catch { config = new AppConfig(); }
        }

        private void ApplyConfigToUI()
        {
            usernameBox.Text = config.Usernames;
            apiKeyBox.Text = config.ApiKey;
            outputFolderBox.Text = config.OutputFolder;
            sortComboBox.SelectedItem = config.Sort;
            periodComboBox.SelectedItem = config.Period;
            limitNumericUpDown.Value = Math.Max(limitNumericUpDown.Minimum, Math.Min(limitNumericUpDown.Maximum, config.Limit));
            downloadVideosCheck.Checked = config.DownloadVideos;
            downloadImagesCheck.Checked = config.DownloadImages;
            sfwCheck.Checked = config.SFW;
            nsfwCheck.Checked = config.NSFW;
        }

        private void SaveConfig()
        {
            config.Usernames = usernameBox.Text;
            config.ApiKey = apiKeyBox.Text;
            config.OutputFolder = outputFolderBox.Text;
            config.Sort = sortComboBox.SelectedItem?.ToString() ?? "";
            config.Period = periodComboBox.SelectedItem?.ToString() ?? "";
            config.Limit = (int)limitNumericUpDown.Value;
            config.DownloadVideos = downloadVideosCheck.Checked;
            config.DownloadImages = downloadImagesCheck.Checked;
            config.SFW = sfwCheck.Checked;
            config.NSFW = nsfwCheck.Checked;
            File.WriteAllText(ConfigFile, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void downloadVideosCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!downloadVideosCheck.Checked && !downloadImagesCheck.Checked)
            {
                downloadImagesCheck.Checked = true;
            }
        }

        private void downloadImagesCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!downloadImagesCheck.Checked && !downloadVideosCheck.Checked)
            {
                downloadVideosCheck.Checked = true;
            }
        }

        private void kofiPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                //Ko-fi username
                string kofiUrl = "https://ko-fi.com/danzelus";
                Process.Start(new ProcessStartInfo(kofiUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the URL. Error: {ex.Message}");
            }
        }
    }

    public class AppConfig
    {
        public string Usernames { get; set; } = "";
        public string ApiKey { get; set; } = "";
        public string OutputFolder { get; set; } = "downloaded";
        public string Sort { get; set; } = "Newest";
        public string Period { get; set; } = "AllTime";
        public int Limit { get; set; } = 0;
        public bool DownloadVideos { get; set; } = true;
        public bool DownloadImages { get; set; } = true;
        public bool SFW { get; set; } = true;
        public bool NSFW { get; set; } = false;
    }
}
