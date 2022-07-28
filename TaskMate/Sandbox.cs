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

        public static void TableTest()
        {


            var inPanel = new Panel("This is a test.")
                        .Border(BoxBorder.Square)
                        .Collapse()
                        //.Header("[yellow]Welcome to TaskMate![/]")
                        .HeaderAlignment(Justify.Center);
            var outPanel = new Panel(inPanel)
                        .Border(BoxBorder.Square)
                        .Collapse()
                        //.Header("[yellow]Welcome to TaskMate![/]")
                        .HeaderAlignment(Justify.Center);

            var inTable = new Table()
                .AddColumn("")
                .AddColumn("")
                .AddColumn("")
                .AddColumn("")
                .AddRow("TEST", "TEST", "TEST", "TEST")
                .AddRow("TEST", "TEST", "TEST", "TEST")
                .AddRow("TEST", "TEST", "TEST", "TEST")
                .AddRow("TEST", "TEST", "TEST", "TEST");
            inTable.HeavyBorder();
            inTable.HideHeaders();


            var table = new Table();
            table.NoBorder();
            table.HideHeaders();
            table.BorderColor(1);
            //table.HozrizntalBorder();
            table.AddColumn("1")
                .AddColumn("2"); ;

            //table.AddColumn("3");
            //table.AddColumn("4");

            table.AddRow(inTable, inTable);
            //table.AddRow(inTable, inTable);
            //table.AddRow(inTable, inTable);
            //table.AddRow("test", "test");
            //table.AddRow("test", "test");
            //table.AddRow(inPanel, outPanel);
            //table.AddRow(inPanel, outPanel);

            AnsiConsole.Write(table);
            AnsiConsole.Write(table);
            Console.ReadKey();
        }
    }
}

