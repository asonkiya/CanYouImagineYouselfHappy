INCLUDE globals.ink

-> main

=== main ===
Do you accept the quest. 
    + [Yes] -> accepted
    + [No] -> rejected

=== accepted ===
~ nun = true
Thank you
-> END

=== rejected ===
Fuck you too then
-> END
  