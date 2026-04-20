
```
 ________  ___       ________  ________  ___  __          ___  ________  ________  ___  __       
|\   __  \|\  \     |\   __  \|\   ____\|\  \|\  \       |\  \|\   __  \|\   ____\|\  \|\  \     
\ \  \|\ /\ \  \    \ \  \|\  \ \  \___|\ \  \/  /|_     \ \  \ \  \|\  \ \  \___|\ \  \/  /|_   
 \ \   __  \ \  \    \ \   __  \ \  \    \ \   ___  \  __ \ \  \ \   __  \ \  \    \ \   ___  \  
  \ \  \|\  \ \  \____\ \  \ \  \ \  \____\ \  \\ \  \|\  \\_\  \ \  \ \  \ \  \____\ \  \\ \  \ 
   \ \_______\ \_______\ \__\ \__\ \_______\ \__\\ \__\ \________\ \__\ \__\ \_______\ \__\\ \__\
    \|_______|\|_______|\|__|\|__|\|_______|\|__| \|__|\|________|\|__|\|__|\|_______|\|__| \|__|
                                                                                                 
                                                                                                 
                                                                                                 
 ________  ___  _____ ______   ___  ___  ___       ________  _________  ________  ________       
|\   ____\|\  \|\   _ \  _   \|\  \|\  \|\  \     |\   __  \|\___   ___\\   __  \|\   __  \      
\ \  \___|\ \  \ \  \\\__\ \  \ \  \\\  \ \  \    \ \  \|\  \|___ \  \_\ \  \|\  \ \  \|\  \     
 \ \_____  \ \  \ \  \\|__| \  \ \  \\\  \ \  \    \ \   __  \   \ \  \ \ \  \\\  \ \   _  _\    
  \|____|\  \ \  \ \  \    \ \  \ \  \\\  \ \  \____\ \  \ \  \   \ \  \ \ \  \\\  \ \  \\  \|   
    ____\_\  \ \__\ \__\    \ \__\ \_______\ \_______\ \__\ \__\   \ \__\ \ \_______\ \__\\ _\   
   |\_________\|__|\|__|     \|__|\|_______|\|_______|\|__|\|__|    \|__|  \|_______|\|__|\|__|  
   \|_________|                                                                                  
                                                                                                 
```
## Blackjack Dealer Simulator
A Windows Forms training application built in C# that simulates a casino blackjack table from the dealer's perspective. Designed to help new casino dealers learn procedure, card handling, and payout rules in a low-pressure environment before working a real table.

## Purpose
Most blackjack training focuses on player strategy. This simulator flips that — the user plays the role of the dealer. Automatic players make decisions using basic strategy while the dealer trainee controls the pace of the game: dealing cards, deciding when to hit or stand on their own hand, and observing how payouts resolve.

## Features

- Dealer-perspective gameplay - you control dealing and own hand, players act automatically
- Basic strategy AI - automatic players follow the standard casino rule (hit below 17, stand on 17+)
- Configurable shoe - choose between 1 and 8 decks
- Configurable player count - 1 to 3 players at the table
- Automatic payout resolution - wins 2×, pushes 1×, and losses 0×
- How to Play reference - in-app image guide accessible from both the menu and the game screen
- Card image display - real card images rendered per player and dealer
- Test panel that lives in testing branch - a separate testing window for verifying core class logic (Deck, Shoe, Hand, Dealer, Player)


## Project structure
Blackjack_course/
├── Card.cs          
├── Deck.cs          
├── Shoe.cs         
├── Hand.cs          
├── Player.cs       
├── Dealer.cs        
├── Table.cs      
├── Form1.cs         
├── Testing.cs       
├── HowToPlayForm.cs 
├── Program.cs       
└── Resources/
    ├── cards/     
    ├── gamescreenbg.png
    └── how_to_play_blackjack.png

## How to run
Requirements

Windows 10 or later
Visual Studio 2022
.NET Framework (Windows Forms)

### Steps

- Clone or download the repository
- Open Blackjack_course.sln in Visual Studio 2022
- Ensure all images in Resources/ have Copy to Output Directory set to Copy always in the file properties
- Press F5 to build and run


## Game flow
New game start
|

Betting (automatic, players place fixed bets)
|

Dealing (dealer distributes 2 cards to each player and self)
|

Player turns (automatic, each player hits or stands by basic strategy)
|

Dealer turn (user decides, press Hit or Stand)
|

Resolving (automatic, compare totals, pay out chips)
|

Done → press new round to continue

## Card naming convention
Card images must follow this exact format:
- {Rank}_of_{Suit}.png
- Examples: Two_of_Spades.png, Ace_of_Hearts.png, King_of_Diamonds.png
- Rank names match the Rank enum: Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
- Suit names match the Suit enum: Spades, Hearts, Diamonds, Clubs

## Blackjack rules implemented
- Cards values 2–10, Jack/Queen/King = 10, Ace = 11 or 1
- BustTotal exceeds 21
- Dealer must hit below 17
- Natural blackjack Ace + face card on first two cards (detected, not yet paid at 3:2)
- PushEqual returns the bet Win
- Player total beats dealer - pays 2× betLose
- Player total below dealer - bet lost

## Testing panel
Switch to testing branch. Launch the test panel from the main menu. Each button runs an isolated test and prints pass/fail results to the log console.

## Known limitations and future improvements:

- Blackjack pays 1:1 instead of the standard casino 3:2
- Only two card PictureBoxes per player - third cards from hits are not displayed
- No split or double-down actions
- Fixed bet amount per round (no variable betting)


## Built with

C# / .NET Framework
Windows Forms (WinForms)
Visual Studio 2022
