using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace MyMaui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    async void OnCameraClickedAsync(object sender, EventArgs e)
    {
        if (!MediaPicker.Default.IsCaptureSupported)
        {
            return;
        }

        AuthenticationRequestConfiguration request = new("Prove you have fingers!", "Because without it you can't have access");
        FingerprintAuthenticationResult result = await CrossFingerprint.Current.AuthenticateAsync(request);

        if (!result.Authenticated)
        {
            return;
        }

        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo is not null)
        {
            string targetFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

            await using Stream sourceStream = await photo.OpenReadAsync();
            await using FileStream outputStream = File.OpenWrite(targetFile);

            await sourceStream.CopyToAsync(outputStream);
        }
    }

    async void OnPickClickedAsync(object sender, EventArgs e)
    {
        await MediaPicker.Default.PickPhotoAsync();
    }
}
