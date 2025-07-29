 DiceGame

A simple, fresh C# .NET Console Application for experimenting with dice game logic.

Features

- Fresh, clean codebase for C# .NET 8.0 Console App
- Easy to customize and extend for your own dice game logic
- Accepts command-line arguments for flexible input
- Starter `Program.cs` included for quick testing
- `.gitignore` included to keep your repo clean
- Well-documented, beginner-friendly structure

Recent Changes

- **Removed previous custom random logic and custom table generation:**  
  The old custom random (random logic) and custom table generation features have been removed.
- **New, simplified codebase:**  
  The project now contains only clean, basic logic—ready for new updates or your own implementation.
- **All unnecessary/legacy code has been deleted:**  
  You can now start fresh without any leftover code, merge conflicts, or unwanted features.


Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later

 Build and Run

1. **Build the Project**
    ```bash
    dotnet build
    ```

2. **Run the Project**
    ```bash
    dotnet run
    ```

3. **(Optional) Pass Command Line Arguments**
    ```bash
    dotnet run -- 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3
    ```

Folder Structure

```
DiceGame/
├── DiceGame.csproj
├── Program.cs
├── .gitignore
└── README.md
```

Customization

- Edit `Program.cs` or add new files to implement your own dice game logic.
- Use command-line arguments for flexible input.
- Add new classes as needed.

License

This project is open source and free to use.

