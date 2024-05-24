## Running the program
1. Ensure that Genshin window has aspect ratio of 16:9 and not in fullscreen mode. 
1. Ensure that the artifact strongbox selection page is opened:
    ![artifact selection page](https://i.imgur.com/0n16YzA.png)
1. Ensure that first row of artifacts is properly aligned:
    Correct:
    ![correct alignment](https://i.imgur.com/0i1nbEU.png)
    Incorrect:
    ![incorrect alignment](https://i.imgur.com/re5tC4G.png)
1. Open the terminal and navigate to directory where the program is located.
1. Run the following command:
`.\ArtifactSelector.exe <sourcefile>`
e.g., `.\ArtifactSelector.exe source.txt`
1. The program will stop when 39 artifacts have been selected or when the mouse is moved.

## Source file

### Meta symbols:
```
a* - repetition 0 or more times of a
a+ - repetition 1 or more times of a
a | b - a or b
() - groupings
```

### Grammar rules:
```
source: statement* return
statement: if | declare | return

if: if_block | if_line
if_block: 'if' '(' bool_expr ')' '{' statement* '}'
if_line: 'if' '(' bool_expr ')' '=>' action ';'
declare: identifier '=' bool_expr ';'
return: 'return' action ';'

action: 'Trash' | 'Keep' | 'Lock'

identifier: (A-Z | a-z)+

bool_expr: bool_cond | 
           '!' bool_expr | 
           bool_expr '&&' bool_expr | 
           bool_expr '||' bool_expr | 
           '(' bool_expr ')'

bool_cond: identifier | 
           'Slot' '(' slot ')' | 
           'Stat' '(' main ')' | 
           'Main' '(' main ')' | 
           'Sub' '(' sub ')' | 
           'Set' '(' set ')' | 

slot: 'FLOWER' | 'PLUME' | 'SANDS' | 'GOBLET' | 'CIRCLET'
main: 'HP' | 'HP%' | 'ATK' | 'ATK%' | 'DEF' | 'DEF%' | 'EM' | 'ER' | 
      'PYRO' | 'CRYO' | 'HYDRO' | 'ELECTRO' | 'DENDRO' | 'GEO' | 'ANEMO' |
      'PHYSICAL' | 'CRITRATE' | 'CRITDMG' | 'HEALING'
sub: 'THREE' | 'FOUR' | 'HP' | 'HP%' | 'ATK' | 'ATK%' | 'DEF' | 'DEF%' | 
     'EM' | 'ER' | 'CRITRATE' | 'CRITDMG'
set: 'GLADIATORSFINALE' | 'WANDERERSTROUPE' | 'NOBLESSEOBLIGE' |
     'BLOODSTAINEDCHIVALRY' | 'MAIDENBELOVED' | 'VIRIDESCENTVENERER' |
     'ARCHAICPETRA' | 'RETRACINGBOLIDE' | 'THUNDERSOOTHER' |
     'THUNDERINGFURY' | 'LAVAWALKER' | 'CRIMSONWITCHOFFLAMES' |
     'BLIZZARDSTRAYER' | 'HEARTOFDEPTH' | 'TENACITYOFTHEMILLELITH' |
     'PALEFLAME' | 'SHIMENAWASREMINISCENCE' | 'EMBLEMOFSEVEREDFATE' |
     'HUSKOFPOLENTDREAMS' | 'OCEANHUEDCLAM' | 'VERMILLIONHEREAFTER' |
     'ECHOESOFANOFFERING' | 'DEEPWOODMEMORIES' | 'GILDEDDREAMS' |
     'DESERTPAVILIONCHRONICLE' | 'FLOWEROFPARADISELOST' |'NYMPHSDREAM' |
     'VOURUKASHASGLOW' | 'MARECHAUSSEEHUNTER' | 'GOLDENTROUPE' |
     'SONGOFDAYSPAST' | 'NIGHTTIMEWHISPERSINTHEECHOINGWOODS'
```

### Sample source file
```
#######################################################
# Declarations
#######################################################

hasCrit = Stat(CRITRATE) || Stat(CRITDMG);

#######################################################
# Generic rules
#######################################################

# Double crit
if (Stat(CRITDMG) && Stat(CRITRATE) && !Slot(CIRCLET)) => Lock;
if ((Main(CRITDMG) || Main(CRITRATE)) && (Sub(CRITRATE) || Sub(CRITDMG))) => Lock;

#######################################################
# Set-specific rules
#######################################################

# Golden Troupe Set
if (Set(GOLDENTROUPE)) 
{
    if (Slot(SANDS)) {
        if (Main(HP%) && hasCrit) => Lock;
    }
    if (!hasCrit) => Trash;
    return Keep;
}

return Keep;
```

## Test file

### Testing your source file

You can test your source using test file to ensure the output is as expected.
`.\ArtifactSelector.exe <sourcefile> -t <testfile>`

### Format of test file

```
name of testcase
set
slot
sub1
sub2
sub3
sub4 or None
expected action
```

### Sample test file
```
1
GLADIATORSFINALE
PLUME
ATK
ATK%
DEF
DEF%
ER
Keep

2
WANDERERSTROUPE
SANDS
ATK%
ATK
DEF
DEF%
None
Trash
```