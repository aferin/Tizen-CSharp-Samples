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

using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using PlayerSample.Models;
using Tizen.Wearable.CircularUI.Forms;
using System.Threading.Tasks;
using System;

namespace PlayerSample.ViewModels
{
    /// <summary>
    /// ViewModel class for the Main Page
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
        public ICommand PrepareCommand { get; protected set; }
        public ICommand PlayCommand { get; protected set; }
        public ICommand StreamInfoCommand { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayPageViewModel"/> class
        /// </summary>
        public MainPageViewModel()
        {
            Tizen.Multimedia.AudioStreamPolicy audioStreamPolicy = new Tizen.Multimedia.AudioStreamPolicy(Tizen.Multimedia.AudioStreamType.Media);
            MediaPlayer.SetSource("/home/owner/media/Videos/Color.mp4");
            //MediaPlayer.ApplyAudioStreamPolicy(audioStreamPolicy);

            Display = MediaPlayer.CreateDisplay();
            PlayerState = MediaPlayer.State;

            //MediaPlayer.Buffering += OnBuffering;
            MediaPlayer.ErrorOccurred += OnErrorOccurred;
            //MediaPlayer.SubtitleUpdated += OnSubtitleUpdated;
            MediaPlayer.StateChanged += OnStateChanged;

            InitializeCommands();
            Test().ConfigureAwait(false);
           
            /* test */
            while (true)
            {
                if (PlayerState == MediaPlayerState.Ready)
                {
                    Logger.Log("Player Start");
                    MediaPlayer.Start();
                    break;
                }
            }
        }

        async public Task Test()
        {
            await Task.Run(() => MediaPlayer.PrepareAsync());
        }

        private void InitializeCommands()
        {
            PrepareCommand = new Command(async () =>
            {
                Logger.Log("@@@@@Prepare");
                try
                {
                    await MediaPlayer.PrepareAsync();
                }
                catch (Exception e)
                {
                    ErrorText = e.Message;
                }
            });

            PlayCommand = new Command(() =>
            {
                if (PlayerState == MediaPlayerState.Ready)
                {
                    Logger.Log("@@@@@Play");
                    MediaPlayer.Start();
                }
                else
                {
                    Logger.Log("@@@@@Stop");
                    MediaPlayer.Stop();
                }
            });

            StreamInfoCommand = new Command(() =>
            {
                Logger.Log("@@@@@StreamInfo");
                Properties = Properties == null ? MediaPlayer.GetStreamInfo() : null;
                var _textPopUp = new InformationPopup();
                _textPopUp.Text = Properties.ToString();

                _textPopUp.BackButtonPressed += (s, e) =>
                {
                    _textPopUp.Dismiss();
                };
            });
            Logger.Log("@@@@@ Leave InitianlizeCommands");
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            PlayerState = MediaPlayer.State;

            switch (PlayerState)
            {
                case MediaPlayerState.Playing:
                    break;

                case MediaPlayerState.Paused:
                    break;

                case MediaPlayerState.Ready:
                    break;
            }
            OnPropertyChanged(nameof(PlayText));
        }
        private void OnErrorOccurred(object sender, ErrorEventArgs e)
        {
            ErrorText = e.Message;
        }

        protected IMediaPlayer MediaPlayer => DependencyService.Get<IMediaPlayer>();
        public string PlayText => PlayerState == MediaPlayerState.Ready ? "Play" : "Stop";

        private MediaPlayerState _playerState;

        public MediaPlayerState PlayerState
        {
            get => _playerState;
            set
            {
                if (_playerState != value)
                {
                    _playerState = value;

                    OnPropertyChanged(nameof(PlayerState));
                }
            }
        }
        public Tizen.Multimedia.Display Display { get; }

        private IEnumerable<Property> _properties;

        public IEnumerable<Property> Properties
        {
            get => _properties;
            protected set
            {
                if (_properties != value)
                {
                    _properties = value;

                    OnPropertyChanged(nameof(Properties));
                }
            }
        }

        private string _errorText;

        public string ErrorText
        {
            get => _errorText;
            protected set
            {
                if (_errorText != value)
                {
                    _errorText = value;

                    OnPropertyChanged(nameof(ErrorText));
                }
            }
        }
    }
}
