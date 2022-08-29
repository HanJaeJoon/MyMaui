namespace MyMaui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    async void OnCameraClickedAsync(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream outputStream = File.OpenWrite(targetFile);

                await sourceStream.CopyToAsync(outputStream);
            }
        }
    }
}
