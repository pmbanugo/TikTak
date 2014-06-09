TikTak
======

TikTak is a classic game of Tic Tac Toe that can be played between a human player and the computer. The computer player is designed with an unbeatable AI where it never loses against it's human opponent. Play with the computer and discover strategies for winning a game or making a tie. Enjoy!

How to run the code -
=====================
This is a windows forms application built with Visual Studio 2012. To run the code:
- open the TikTak.csproj file (This opens the project in Visual Studio)
- Build the project
- Run (F5 or Ctrl-F5)

Adding extra User Interface
===========================
The code has separated the UI specific concern from the basic game implementation, which opens the door for addition of UI projects/presentation e.g WPF or WebForms, which will work with the Game Logic. To do this:
- Add the presentation project e.g WinForms or Console.
- Add Reference to the TikTak.Core.
- Create intances of IGameService and IController. The Controller controls each players move. An example is contained in UI.Winforms.GameUI.cs file.


Enjoy!