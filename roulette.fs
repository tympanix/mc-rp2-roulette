1 BLOCK WIPE
0 PP : install 8 2 DO CR ." Installing: " I . ." of 7"
1 PP I LOAD LOOP CR ." Done!" ;
FLUSH

2 BLOCK WIPE
0 PP HEADER M+
1 PP 250 ,C 104 ,C 24 ,C 99 ,C 2 ,C 131 ,C 2 ,C 138 ,C
2 PP 99 ,C 0 ,C 131 ,C 0 ,C 2 ,C
3 PP HEADER M-
4 PP 163 ,C 6 ,C 56 ,C 227 ,C 2 ,C 131 ,C 6 ,C 163 ,C
5 PP 4 ,C 227 ,C 0 ,C 131 ,C 4 ,C 250 ,C 250 ,C 2 ,C
6 PP : M= ROT = -ROT = AND ;
7 PP HEADER M!
8 PP 250 ,C 104 ,C 149 ,C 2 ,C 104 ,C 149 ,C 0 ,C 2 ,C
9 PP HEADER M@
10 PP 250 ,C 181 ,C 0 ,C 72 ,C 181 ,C 2 ,C 72 ,C 2 ,C
FLUSH

3 BLOCK WIPE
0 PP : on? DUP IOX@ AND = ; : var VARIABLE ; var $current
1 PP var $seed 34175 $seed ! : random $seed @ 31421 * 6927
2 PP + DUP $seed ! ; : choose random UM* SWAP DROP ;
3 PP : roll 7 IOXADDR ! 33 choose 22 + 0 DO $current @ 15 = IF
4 PP 0 $current ! ELSE 1 $current +! THEN 1 $current @ << IOX!
5 PP 5 TICKS LOOP ; var $max 10000 $max ! var $currem var $wins
6 PP var $income var $ic var $iron var $gold var $dia
7 PP var $ironb var $goldb var $diab var $wait 10 $wait !
8 PP : rstline 16384 IOXSET 10 TICKS 16384 IOXRST ;
9 PP var $delay 60 $delay ! : lamp 5 IOXADDR ! IOX@ ;
10 PP var $min 128 $min ! : checkbet $max @ $currem @ -
11 PP $min @ ; : bin 3 IOXADDR ! 4 IOXSET 16 IOXRST ;
12 PP : ain 3 IOXADDR ! 15 IOXRST ; : win 3 IOXADDR ! 1 $wins +!
13 PP 8 IOXSET ; CREATE $import 2 CELLS ALLOT 0 0 $import M!
14 PP CREATE $export 2 CELLS ALLOT 0 0 $export M!
15 PP var $maxbet var $lost
FLUSH


4 BLOCK WIPE
0 PP : clearslot 2 SORTCOLOR! 64 SWAP SORTPULL DROP ; : chkmax
1 PP DUP $currem @ SWAP MOD DUP 64 > IF DROP 64 THEN ROT 1
2 PP SORTCOLOR! SORTPULL DUP -ROT U* DUP $income +! $currem -! ;
3 PP : cashin 4 SORTADDR ! SORTSLOTS
4 PP 0 DO I SORTSLOT@ 0<> IF +
5 PP DUP 5359 = IF I 1 chkmax $ic +! ELSE
6 PP DUP -9 = IF I 4 chkmax $iron +! ELSE
7 PP DUP 2958 = IF I 32 chkmax $gold +! ELSE
8 PP DUP 6191 = IF I 128 chkmax $dia +! ELSE
9 PP DUP -31156 = IF I 36 chkmax $ironb +! ELSE
10 PP DUP -28189 = IF I 288 chkmax $goldb +! ELSE
11 PP DUP 30584 = IF I 1152 chkmax $diab +! ELSE
12 PP I clearslot THEN THEN THEN THEN THEN THEN THEN
13 PP THEN 0SP LOOP ;
FLUSH

5 BLOCK WIPE
0 PP : draw /MOD SWAP ; : ddraw UM/MOD SWAP ;
1 PP : dispense OVER 64 /MOD DUP 0<> IF 0 DO 64 2OVER SORTPULL
2 PP DROP $wait @ TICKS LOOP ELSE DROP THEN OVER SORTPULL 2DROP
3 PP DROP ; : cashout 18432 ddraw 4608 draw 576 draw 256 draw
4 PP 6 SORTADDR ! 0 dispense 64 * 1 dispense 16 * 2 dispense
5 PP 16 * 3 dispense 16 * 4 dispense ;
6 PP : nobet 3 IOXADDR ! 16 IOXSET ; : imp+! 0 $import
7 PP M@ M+ $import M! ; : updmax $maxbet @ MAX $maxbet ! ;
8 PP : rstcm $max @ $currem ! ; : lost 1 $lost +! ;
9 PP : M. 10000 UM/MOD . BS 1000 /MOD . BS 100 /MOD . BS
10 PP 10 /MOD . BS . ;
FLUSH

6 BLOCK WIPE
0 PP : start rstcm BEGIN ain BEGIN rstline BEGIN $delay @ TICKS
1 PP IOX@ 32771 AND UNTIL 1 on? IF cashin THEN 32768 on? IF CR
2 PP ." Roulette Stopped!" EXIT THEN 2 on? UNTIL lamp 0<> IF
3 PP checkbet < IF cashin THEN checkbet >= IF bin roll lamp 1
4 PP $current @ << = IF win $max @ $currem @ - 10 UM* 2DUP
5 PP $export M@ M+ $export M! cashout ELSE lost THEN $max @
6 PP $currem @ - DUP imp+! updmax rstcm ELSE nobet THEN ELSE
7 PP CR ." No user choice!" THEN AGAIN ;
FLUSH

7 BLOCK WIPE
0 PP : stats PAGE CR ." Total import: " $import M@ M.
1 PP CR ." Total export: " $export M@ M.
2 PP CR CR ." Industrial Credits: " $ic @ . CR
3 PP ." Iron ingots: " $iron @ . CR ." Iron blocks: "
4 PP $ironb @ . CR ." Gold ingots: " $gold @ . CR
5 PP ." Gold blocks: " $goldb @ . CR ." Diamonds : "
6 PP $dia @ . CR ." Diamond blocks: " $diab @ .
7 PP CR CR ." Total spins: " $lost @ $wins @ + .
8 PP CR ." Total wins: " $wins @ . CR ." Total lost: " $lost @ .
9 PP CR CR ." Maximum bet: " $max @ .
10 PP CR ." Highest player bet: " $maxbet @ . CR ;
FLUSH
