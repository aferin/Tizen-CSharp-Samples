//Copyright 2019 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using MediaToolSample.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Multimedia;

namespace MediaToolSample.ViewModels
{
    /// <summary>
    /// ViewModel class for the Main Page
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets command for pushing new page
        /// </summary>
        public ICommand CreateFormatCommand { get; protected set; }
        public ICommand CreatePacketCommand { get; protected set; }
        public ICommand OtherFormatCommand { get; protected set; }
        public ICommand DestroyPacketCommand { get; protected set; }
        public ICommand GetFormatCommand { get; protected set; }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; }

        private MediaFormat _mediaformat;
        public MediaFormat SimpleFormat
        {
            get => _mediaformat;
            protected set
            {
                if (_mediaformat != value)
                {
                    _mediaformat = value;

                    OnPropertyChanged(nameof(MediaFormat));
                }
            }
        }

        private MediaPacket _mediapacket;
        public MediaPacket MediaPacket
        {
            get => _mediapacket;
            protected set
            {
                if (_mediapacket != value)
                {
                    _mediapacket = value;

                    OnPropertyChanged(nameof(MediaPacket));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            InitializeCommands();
        }
        private void InitializeCommands()
        {
            CreateFormatCommand = new Command(async () =>
            {
                try
                {
                    SimpleFormat = new AudioMediaFormat(MediaFormatAudioMimeType.Aac, 1, 2, 3, 4);
                    Toast.DisplayText("MediaFormat is Created", 1000);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            CreatePacketCommand = new Command(async () =>
            {
                try
                {
                    if (MediaPacket != null)
                        throw new Exception("MediaPacket is already created");

                    MediaPacket = MediaPacket.Create(SimpleFormat);
                    Toast.DisplayText("MediaPacket is Created", 1000);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            OtherFormatCommand = new Command(async () =>
            {
                try
                {
                    MediaFormat VideoFormat = new VideoMediaFormat(MediaFormatVideoMimeType.Argb, 320, 240, 50000, 300000);
                    MediaPacket = MediaPacket.Create(VideoFormat);
                    Toast.DisplayText("MediaPacket is Created with VideoFormat", 1000);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            DestroyPacketCommand = new Command(async () =>
            {
                try
                {
                    if (MediaPacket.IsDisposed)
                        throw new Exception("MediaPacket is already destroyed");

                    MediaPacket.Dispose();
                    Toast.DisplayText("MediaPacket is destroyed", 1000);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            GetFormatCommand = new Command(async () =>
            {
                try
                {
                    Toast.DisplayText(MediaPacket.Format.ToString(), 1000);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
        }
    }
}
