using System;
using System.IO;
using Spectre.Console;

namespace TaskMate
{
    public static class Sandbox
    {
        public static void PanelTest()
        {
            

            
            var content = new Markup(File.ReadAllText("WriteText.txt")).Centered();

            AnsiConsole.Write(
                new Panel(
                    new Panel(content)
                        .Border(BoxBorder.Rounded)
                        .Collapse()
                        .Header("[yellow]Welcome to TaskMate![/]")
                        .HeaderAlignment(Justify.Center)
                        ));


            // Left adjusted panel with text
            AnsiConsole.Write(
                new Panel(new Text("Left adjusted\nLeft").LeftAligned())
                    .Expand()
                    .SquareBorder()
                    .Header("[red]Left[/]"));

            // Centered ASCII panel with text
            AnsiConsole.Write(
                new Panel(new Text("Centered\nCenter").Centered())
                    .Expand()
                    .AsciiBorder()
                    .Header("[green]Center[/]")
                    .HeaderAlignment(Justify.Center));

            // Right adjusted, rounded panel with text
            AnsiConsole.Write(
                new Panel(new Text("Right adjusted\nRight").RightAligned())
                    .Expand()
                    .RoundedBorder()
                    .Header("[blue]Right\n[/]")
                    .HeaderAlignment(Justify.Right));
        }
    }
}

