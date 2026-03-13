using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using TileProfile.Library.Models;
using TileProfile.Library.Utils;

namespace TileProfile.Avalonia;

public partial class MainWindow : Window
{
    private DispatcherTimer _timer;
    private List<WindowProfile> _windowProfiles;
    
    public MainWindow()
    {
        InitializeComponent();
        
        // _timer = new DispatcherTimer();
        // _timer.Interval = TimeSpan.FromMilliseconds(200);
        // _timer.Tick += (sender, args) => RefreshActive(sender, null);
        // _timer.Start();
        
        _windowProfiles = new List<WindowProfile>()
        {
            new WindowProfile
            {
                Name = "Default Profile 1",
                MainWindowWidth = 800,
                MainWindowHeight = 600,
                WindowBoxes = new List<WindowBox>
                {
                    new WindowBox { Index = 1, Width = 400, Height = 300, X = 100, Y = 100 },
                    new WindowBox { Index = 2, Width = 400, Height = 300, X = 500, Y = 500 },
                }
            }
        };
        
        LoadBoxesIntoTextBox(_windowProfiles.First());
    }
    
    // public void RefreshActive(object sender, RoutedEventArgs args)
    // {
    //     string activeWindow = WmCtrlUtils.GetCurrentActiveWindowTitle();
    //     TbActiveWindow.Text = activeWindow;
    // }
    
    public void SaveProfile(object sender, RoutedEventArgs args)
    {
        Console.WriteLine($"Attempting to save ({CbProfiles.Text}) Window Profile");
        // RefreshActive(sender, null);
        Console.WriteLine($"Successfully saved ({CbProfiles.Text}) Window Profile");
    }
    
    public void UndoProfile(object sender, RoutedEventArgs args)
    {
        Console.WriteLine($"Attempting to undo ({CbProfiles.Text}) Window Profile");
        Console.WriteLine($"Successfully undid ({CbProfiles.Text}) Window Profile");
    }
    
    public void ApplyBox(object sender, RoutedEventArgs args)
    {
        string? selectedItem = ((string)LbAllProcesses.SelectedItem!)?.Trim();
        Console.WriteLine($"Attempting to apply ({selectedItem}) Window Profile");
        WmCtrlUtils.ApplyWindowBox(selectedItem, _windowProfiles.First().WindowBoxes.First());
        Console.WriteLine($"Successfully applied ({selectedItem}) Window Profile");
    }

    private void LoadBoxesIntoTextBox(WindowProfile profile)
    {
        TbAllBoxes.Text = profile.GetBoxesAsWmCtrlString();
        List<string> windows = WmCtrlUtils.GetActiveWindows();
        // TbAllProcesses.Text = string.Join('\n', windows); 
        foreach (string window in windows)
        {
            LbAllProcesses.Items.Add(window);
        }
    }

    // public void SetNumberOfBoxes(object sender, RoutedEventArgs args)
    // {
    //     
    // }
}