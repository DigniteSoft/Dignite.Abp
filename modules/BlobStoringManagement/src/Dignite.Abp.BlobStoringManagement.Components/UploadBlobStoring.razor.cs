using Blazorise;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoringManagement.Components
{
    public partial class UploadBlobStoring : Blazorise.FileEdit
    {
        /// <summary>
        /// 容器名称
        /// </summary>
        [Parameter]
        public string ContainerName { get; set; }
        /// <summary>
        /// 实体类型
        /// </summary>
        [Parameter]
        public string EntityType { get; set; } = "common-attachment";
        /// <summary>
        /// 按钮样式
        /// </summary>
        [Parameter]
        public RenderFragment Content { get; set; }

        /// <summary>
        /// 是否自动上传
        /// </summary>
        [Parameter]
        public bool AutoUpload { get; set; }

        public double Percentage { get; set; }

        public EventCallbackFactory EventFactory { get; set; }

        public List<SaveStreamInput> Files { get; set; } = new List<SaveStreamInput>();

        public UploadBlobStoring()
        {
            EventFactory = new EventCallbackFactory();
            Changed = EventFactory.Create<FileChangedEventArgs>(this, OnChanged);
            Progressed = EventFactory.Create<FileProgressedEventArgs>(this, OnProgressed);
        }

        protected override Task OnInternalValueChanged(IFileEntry[] value)
        {
            return base.OnInternalValueChanged(value);
        }
        CancellationTokenSource cancellation;
        async Task OnChanged(FileChangedEventArgs e)
        {
            try
            {
                Files = new List<SaveStreamInput>();
                Percentage = 0;
                foreach (var file in e.Files)
                {
                    if (AutoUpload)
                    {

                    }
                    byte[] buffer = new byte[file.Size];
                    var stream1 = file.OpenReadStream(file.Size);
                    int bytesRead = 0;
                    var totalBytesRead = 0;
                    while ((bytesRead = await stream1.ReadAsync(buffer, cancellation.Token)) != 0)
                    {
                        totalBytesRead += bytesRead;
                        using MemoryStream tmpMemoryStream = new MemoryStream(buffer, 0, bytesRead);
                        using MultipartFormDataContent content = new();
                        content.Add(
                            content: new StreamContent(tmpMemoryStream, Convert.ToInt32(tmpMemoryStream.Length)),
                            name: "\"uploadFile\"",
                            fileName: upload.UniqueFileName
                            );
                        var response = await client.PostAsync("/api/Upload", content);
                        response.EnsureSuccessStatusCode();

                        var fileLocation = response.Headers.Location.ToString();

                        MultipartFormDataContent content = new MultipartFormDataContent();
                        var EntityType = "common-attachment";
                        var EntityId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        content.Add(new StringContent(EntityType), "EntityType");
                        content.Add(new StringContent(EntityId), "EntityId");

                        var streamContent = new StreamContent(stream1);
                        streamContent.Headers.ContentLength = file.Size;
                        content.Add(new StreamContent(stream1, (int)stream1.Length), "FileStream", file.Name);
                        try
                        {
                            var client = httpClientFactory.CreateClient();
                            var response = await client.PostAsync($"https://localhost:44341/api/blob-storing/blobs/save/{ContainerName}", content);
                            Console.WriteLine(response);
                        }
                        catch (Exception ex)
                        {

                        }
                        StateHasChanged();
                    };
                    

                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                this.StateHasChanged();
            }
        }

        void OnProgressed(FileProgressedEventArgs e)
        {
            Percentage = e.Percentage;
            Console.WriteLine($"File: {e.File.Name} Progress: {e.Percentage}");
            this.StateHasChanged();
        }


        public async Task OnClick()
        {
            Console.WriteLine($"开始上传...");
            foreach (var item in Files)
            {
                //var client = httpClientFactory.CreateClient();
                //MultipartFormDataContent content = new MultipartFormDataContent();
                //content.Add(new StreamContent(fStream, (int)fStream.Length), "file", fileName);
                //var result = await client.PostAsync($"uploadstream", httpContent);
                //Console.WriteLine(result);
            }
        }
    }
}
