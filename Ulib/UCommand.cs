using Rocket.API;
using Rocket.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib
{
   public abstract class UCommand : IRocketCommand
    {
        public UCommand(int ACaller = 1, string Command = "", string help = "", string syntax = "", string aliases = "", string permissions = "")
        {
            AllowedCaller = (AllowedCaller)ACaller;
            Name = Command;
            Help = help;
            Syntax = syntax;
            Aliases = new List<string>(aliases.Split(','));
            Permissions = new List<string>(permissions.Split(','));
        }

        public AllowedCaller AllowedCaller { get; }

        public string Name { get; }

        public string Help { get; }

        public string Syntax { get; }

        public List<string> Aliases { get; }

        public List<string> Permissions { get; }

        public abstract void Execute(IRocketPlayer caller, string[] command);
    }
    public abstract class Command : IRocketCommand
    {
        CommandType command;
        string[] alliases;
        string[] permssions;
        public Command(CommandType commandType )
        {
            command = commandType;
            alliases = command.Aliasses.Split(',');
            permssions = command.Permissions.Split(',');
        }

        public AllowedCaller AllowedCaller => command.AllowedCaller;

        public string Name => command.Name;

        public string Help => command.Help;

        public string Syntax => command.Syantax;

        public List<string> Aliases => alliases.ToList<string>();

        public List<string> Permissions => permssions.ToList<string>();

        public abstract void Execute(IRocketPlayer caller, string[] command);

    }
    public class CommandType
    {
        public AllowedCaller AllowedCaller { get; set; }
        public string Name { get; set; }
        public string Help { get; set; }
        public string Syantax { get; set; }
        public string Aliasses { get; set; }
        public string Permissions { get; set; }
    }

}
