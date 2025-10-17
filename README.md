# ScreenFlow

A modular UI screen navigation system for Unity projects using UI Toolkit.

## Features

- **Singleton ScreenManager**: Centralized screen management with easy navigation
- **Abstract ScreenUI Base Class**: Extensible base for creating custom screens
- **Type-Based Navigation**: Navigate between screens using string identifiers
- **UI Toolkit Integration**: Built on Unity's modern UI Toolkit (VisualElement)
- **VisualTreeAsset Support**: Use Unity's UI Builder to design screen layouts
- **Default Screen Configuration**: Set a default screen to show on startup
- **Error Handling**: Clear error messages for missing screens

## Installation

1. Open your Unity project
2. Go to Window > Package Manager
3. Click the "+" button and select "Add package from disk..."
4. Navigate to and select the `package.json` file in this package

Or add this line to your `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.crazydev007.screenflow": "file:../path/to/Screen-Flow/Packages/com.crazydev007.screenflow"
  }
}
```

## How to Use

### 1. Create Screen Classes

Create subclasses of `ScreenUI` for each screen in your app:

```csharp
using ScreenFlow;
using UnityEngine.UIElements;

public class HomeScreen : ScreenUI
{
    protected override void SetupScreen(VisualElement screen)
    {
        // Setup your screen elements here
        var titleLabel = screen.Q<Label>("title-label");
        var playButton = screen.Q<Button>("play-button");

        playButton.clicked += () => ScreenManager.Instance.ShowScreen("Gameplay");
    }
}
```

### 2. Design UI with UI Builder

1. Create VisualTreeAsset files (.uxml) for each screen using Unity's UI Builder
2. Design your screen layouts visually
3. Save the assets in your project

### 3. Setup ScreenManager

1. Create a GameObject in your scene
2. Add a `UIDocument` component
3. Add the `ScreenManager` component
4. Configure the screen mappings:
   - **Type**: String identifier for the screen (e.g., "Home", "Gameplay")
   - **Screen**: Reference to your ScreenUI component
   - **Is Default**: Check this for the screen to show on startup

### 4. Navigate Between Screens

Use the ScreenManager to navigate. For safe navigation, check if the screen is available before showing it:

```csharp
// Safe navigation - recommended
if (ScreenManager.Instance.GetAvailableScreenTypes().Contains("Gameplay"))
{
    Debug.Log("Gameplay screen is available.");
    ScreenManager.Instance.ShowScreen("Gameplay");
}
else
{
    Debug.Log("Gameplay screen is NOT available.");
}

// Direct navigation (will log error if screen doesn't exist)
ScreenManager.Instance.ShowScreen("Gameplay");

// Check available screens
foreach (var screenType in ScreenManager.Instance.GetAvailableScreenTypes())
{
    Debug.Log($"Available screen: {screenType}");
}
```

## Requirements

- Unity 2021.4 or later
- UI Toolkit package (included in Unity 2021+)

## Keywords

UI, navigation, screens, unity, screenflow, canvas, stack, routing

## Author

CrazyDev007 - gajendramr2@gmail.com

## License

This package is provided as-is for use in Unity projects.

