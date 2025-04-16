﻿using Spectre.Console;

#region Handling cross-platform environments and filesystems

SectionTitle("Handling cross-platform environments and filesystems");

// Create a Spectre Console table.
Table table = new();

// Add two columns with markup for colors.
table.AddColumn("[blue]MEMBER[/]");
table.AddColumn("[blue]VALUE[/]");

// Add rows.
table.AddRow("Path.PathSeparator", PathSeparator.ToString());
table.AddRow("Path.DirectorySeparatorChar",
DirectorySeparatorChar.ToString());
table.AddRow("Directory.GetCurrentDirectory()",
GetCurrentDirectory());
table.AddRow("Environment.CurrentDirectory", CurrentDirectory);
table.AddRow("Environment.SystemDirectory", SystemDirectory);
table.AddRow("Path.GetTempPath()", GetTempPath());
table.AddEmptyRow();
table.AddRow("GetFolderPath(SpecialFolder", "");
table.AddRow(" .System)", GetFolderPath(SpecialFolder.System));
table.AddRow(" .ApplicationData)",
GetFolderPath(SpecialFolder.ApplicationData));
table.AddRow(" .MyDocuments)",
GetFolderPath(SpecialFolder.MyDocuments));
table.AddRow(" .Personal)",
GetFolderPath(SpecialFolder.Personal));
// Render the table to the console
table.Caption("This is my table");
table.HeavyBorder();
// AnsiConsole.Write(table);


SectionTitle("Managing drives");
Table drives = new();
drives.AddColumn("[blue]NAME[/]");
drives.AddColumn("[blue]TYPE[/]");
drives.AddColumn("[blue]FORMAT[/]");
drives.AddColumn(new TableColumn(
"[blue]SIZE (BYTES)[/]").RightAligned());
drives.AddColumn(new TableColumn(
"[blue]FREE SPACE[/]").RightAligned());
foreach (DriveInfo drive in DriveInfo.GetDrives())
{
    if (drive.IsReady)
    {
        drives.AddRow(drive.Name, drive.DriveType.ToString(),
        drive.DriveFormat, drive.TotalSize.ToString("N0"),
        drive.AvailableFreeSpace.ToString("N0"));
    }
    else
    {
        drives.AddRow(drive.Name, drive.DriveType.ToString(),
        string.Empty, string.Empty, string.Empty);
    }
}

drives.RoundedBorder();
// AnsiConsole.Write(drives);


// Create the layout
var layout = new Layout("Root")
    .SplitColumns(
        new Layout("Left"),
        new Layout("Right")
            );

// Update the left column
layout["Left"].Update(table);
layout["Right"].Update(drives);

// Render the layout
AnsiConsole.Write(layout);

SectionTitle("Managing directories");
string newFolder = Combine(
GetFolderPath(SpecialFolder.Personal), "NewFolder");
WriteLine($"Working with: {newFolder}");
// We must explicitly say which Exists method to use
// because we statically imported both Path and Directory.
WriteLine($"Does it exist? {Path.Exists(newFolder)}");
WriteLine("Creating it...");
CreateDirectory(newFolder);
// Let's use the Directory.Exists method this time.
WriteLine($"Does it exist? {Directory.Exists(newFolder)}");
Write("Confirm the directory exists, and then press any key.");
ReadKey(intercept: true);
WriteLine("Deleting it...");
Delete(newFolder, recursive: true);
WriteLine($"Does it exist? {Path.Exists(newFolder)}");


SectionTitle("Managing files");
// Define a directory path to output files starting
// in the user's folder.
string dir = Combine(
GetFolderPath(SpecialFolder.Personal), "OutputFiles");
CreateDirectory(dir);
// Define file paths.
string textFile = Combine(dir, "Dummy.txt");
string backupFile = Combine(dir, "Dummy.bak");
WriteLine($"Working with: {textFile}");
WriteLine($"Does it exist? {File.Exists(textFile)}");
// Create a new text file and write a line to it.
StreamWriter textWriter = File.CreateText(textFile);
textWriter.WriteLine("Hello, C#!");
textWriter.Close(); // Close file and release resources.
WriteLine($"Does it exist? {File.Exists(textFile)}");
// Copy the file, and overwrite if it already exists.
File.Copy(sourceFileName: textFile,
destFileName: backupFile, overwrite: true);
WriteLine(
$"Does {backupFile} exist? {File.Exists(backupFile)}");
Write("Confirm the files exist, and then press any key.");
ReadKey(intercept: true);
// Delete the file.
File.Delete(textFile);
WriteLine($"Does it exist? {File.Exists(textFile)}");
// Read from the text file backup.
WriteLine($"Reading contents of {backupFile}:");
using (StreamReader textReader = File.OpenText(backupFile))
{
    WriteLine(textReader.ReadToEnd());
}

SectionTitle("Managing paths");
WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
WriteLine($"File Name: {GetFileName(textFile)}");
WriteLine("File Name without Extension: {0}",
GetFileNameWithoutExtension(textFile));
WriteLine($"File Extension: {GetExtension(textFile)}");
WriteLine($"Random File Name: {GetRandomFileName()}");
WriteLine($"Temporary File Name: {GetTempFileName()}");

string tmpFile = Combine(Path.GetTempPath(), "tmpoveoyb.tmp");
if (File.Exists(tmpFile))
{
    using (StreamReader textReader = File.OpenText(tmpFile))
    {
        WriteLine(textReader.ReadToEnd());
    }
}

SectionTitle("Getting file information");
FileInfo info = new(tmpFile);
WriteLine($"{tmpFile}:");
WriteLine($" Contains {info.Length} bytes.");
WriteLine($" Last accessed: {info.LastAccessTime}");
WriteLine($" Has readonly set to {info.IsReadOnly}.");

#endregion