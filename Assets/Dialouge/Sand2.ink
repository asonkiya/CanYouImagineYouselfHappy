INCLUDE globals.ink
-> main

=== main ===
The sun made the sand all dry and crumbly.  
I need new sand before I can try again. Can you get some?

+ "Sure." 
    ~ sandcastle = true
    Yay! Make sure it’s wet so it sticks!
    -> DONE

+ "It’s okay if it doesn’t work."
    I know. But I still want to try.
    -> DONE

+ "Maybe you should take a break."
    Then I’ll just be sitting here.
    -> END