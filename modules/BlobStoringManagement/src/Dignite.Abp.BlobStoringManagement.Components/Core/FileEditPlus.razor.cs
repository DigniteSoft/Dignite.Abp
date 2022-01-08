using Blazorise;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoringManagement.Components.Core
{
    public partial class FileEditPlus
    {
        string SelectedFilenamesString => string.Join(", ", SelectedFileNames);
        bool HasSelectedFileNames => SelectedFileNames.Any();

        /// <summary>
        /// 按钮样式
        /// </summary>
        [Parameter]
        public RenderFragment Content { get; set; }

        public string FileName { get; set; }
        public double Percentage { get; set; }

        public EventCallbackFactory EventFactory { get; set; }

        public List<SaveBytesInput> Files { get; set; } = new List<SaveBytesInput>();

        public FileEditPlus()
        {
            EventFactory = new EventCallbackFactory();
            Changed = EventFactory.Create<FileChangedEventArgs>(this, OnChanged);
            Progressed = EventFactory.Create<FileProgressedEventArgs>(this, OnProgressed);
        }

        protected override Task OnInternalValueChanged(IFileEntry[] value)
        {
            return base.OnInternalValueChanged(value);
        }

        async Task OnChanged(FileChangedEventArgs e)
        {
            try
            {
                Files = new List<SaveBytesInput>();
                foreach (var file in e.Files)
                {
                    FileName = file.Name;
                    Percentage = 0;
                    var stream1 = file.OpenReadStream(file.Size);
                    var bytes = await stream1.GetAllBytesAsync();
                    var fileInput = new SaveBytesInput
                    {
                        Bytes = bytes,
                        EntityId = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                        EntityType = "common-attachment",
                        FileName = file.Name
                    };
                    Files.Add(fileInput);
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
        async Task OnProgressed(FileProgressedEventArgs e)
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
                var result = await BlobService.SaveAsync("upload", item);
                Console.WriteLine(result);
            }
        }
    }
}
