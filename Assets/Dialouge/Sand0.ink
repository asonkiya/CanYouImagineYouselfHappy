INCLUDE globals.ink
-> main

=== main ===
I made the best castle ever!  
But the wind knocked it down and now I don’t have enough sand. Can you bring me more?

+ "Of course!" 
    ~ sandcastle = true
    Yay! I’ll start digging the moat while you go!
    -> DONE

+ "Won’t it just fall again?"
    That’s okay! I can always build another one!
    -> DONE

+ "Why not build somewhere safer?"
    But I like it here. It’s my spot!
    -> END