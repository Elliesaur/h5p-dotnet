using H5pDotNet.Libraries.DialogCards;
using H5pDotNet.Types;

namespace H5pDotNet;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Create example.
        var newPackage = await H5PPackage.FromNewPackage(
            "H5P.Dialogcards",
            "hello-world",
            "Hello World");
        var card = new H5PDialogCard
        {
            Text = "Something Text",
            Answer = "Hello"
        };
        var content = new H5PDialogCards
        {
            Title = "Hello Title",
            Description = "Hello Description",
            Dialogs = [card]
        };
        await newPackage.SetMainContent(content);
        await newPackage.Save();
        
        // Load example.
        var loadExample = await H5PPackage.FromExistingPackage(Environment.CurrentDirectory + "/hello-world.h5p");
        var loadContent = loadExample.Content as H5PDialogCards;
        loadContent?.Dialogs.Add(new H5PDialogCard()
        {
            Text= "Second Question",
            Answer = "Second Answer",
            Image = (ImageMediaType?)
                await MediaType.FromRemoteFile(loadExample, 
                    "https://placehold.co/600x400/EEE/31343C.png")
        });
        await loadExample.Save();

    }
}