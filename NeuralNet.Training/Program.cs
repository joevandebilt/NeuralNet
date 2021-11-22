using HtmlAgilityPack;
using NeuralNet.Core.Entities;
using NeuralNet.Core.Entities.Layers;
using NeuralNet.Training.TestingModels;
using NeuralNet.Training.TestingModels.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NeuralNet.Training
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //ITestingModel model = new TextModel();
            //model.RunTestingModel();

            ScrapeScripts().GetAwaiter().GetResult();   
        }

        private static async Task ScrapeScripts()
        {
            string baseAddress = "http://www.chakoteya.net/NextGen/episodes.htm";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                HtmlDocument homepageDoc = new HtmlDocument();

                var response = await client.GetAsync(baseAddress);
                if (response.IsSuccessStatusCode)
                {
                    string pageHtml = await response.Content.ReadAsStringAsync();
                    homepageDoc.LoadHtml(pageHtml);

                    if (homepageDoc != null)
                    {
                        Dictionary<string, string> episodes = new Dictionary<string, string>();
                        var linksToEpisodes = homepageDoc.DocumentNode.SelectNodes("//tr//a");

                        foreach (var linkToEpisode in linksToEpisodes)
                        {
                            string pageLink = linkToEpisode.GetAttributeValue("href", null);
                            string episodeNumber = pageLink.Replace(".htm", string.Empty);

                            if (!string.IsNullOrWhiteSpace(pageLink) && int.TryParse(episodeNumber, out int episodeVal))
                            {
                                var scriptPageResponse = await client.GetAsync(pageLink);
                                if (scriptPageResponse.IsSuccessStatusCode)
                                {
                                    var scriptPageHtml = await scriptPageResponse.Content.ReadAsStringAsync();
                                    var scriptPage = new HtmlDocument();

                                    scriptPage.LoadHtml(scriptPageHtml);

                                    var episodeTitleNode = scriptPage.DocumentNode.SelectSingleNode("//font/b");
                                    string episodeTitle = $"{episodeNumber}_{Regex.Replace(episodeTitleNode.InnerText, "(?:\\W)", string.Empty)}";

                                    string scriptContent = scriptPage.DocumentNode.SelectSingleNode("//div").InnerText;

                                    episodes.Add(episodeTitle, scriptContent);
                                }                                
                            }
                        }

                        Console.WriteLine("I scraped {0} episodes", episodes.Count);

                        foreach(var episode in episodes)
                        {
                            Console.WriteLine("Writing {0} to disk", episode.Key);

                            if (!Directory.Exists("F:\\Output)"))
                                Directory.CreateDirectory("F:\\Output");

                            using (var fileStream = new FileStream($"F:\\Output\\{episode.Key}.txt", FileMode.CreateNew))
                            { 
                                await fileStream.WriteAsync(Encoding.UTF8.GetBytes(episode.Value), 0, episode.Value.Length);
                            }
                        }
                    }
                }
            }
        }
    }
}
