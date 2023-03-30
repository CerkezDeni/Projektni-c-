using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace Projektni
{
    public class AddMusic
    {
        public async System.Threading.Tasks.Task AddMedia(ListView listView, MediaPlayerElement mediaPlayerElement)
        {

            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            string[] fileTypes = new string[] { ".wmv", ".mp3", ".mp4", ".wma" };
            foreach (string fileType in fileTypes)
            {
                filePicker.FileTypeFilter.Add(fileType);
            }

            filePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;

            MediaPlaybackList _mediaPlaybackList = new MediaPlaybackList();

            var pickedFiles = await filePicker.PickMultipleFilesAsync();

            foreach (var file in pickedFiles)
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));
                _mediaPlaybackList.Items.Add(mediaPlaybackItem);
                listView.Items.Add(file.DisplayName);
            }
            _mediaPlaybackList.AutoRepeatEnabled = true;
            mediaPlayerElement.Source = _mediaPlaybackList;
        }
    }
}
