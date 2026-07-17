=== choices ===

= apple_tree
Tree: I am an apple tree. #avatars:apple_tree
*[Yes you are]
~ Event("Drop_Apple")
-> END
*[No you're not]
~ Event("Drop_Grape")
-> END


= goose_talk
Goose of Lie: Hehe, oh hello! May I interest you in answering a riddle for me? I'll even give you something if you get it right! #avatars:goose
+[Sure thing! Hit me with your riddle]
-> goose_riddle

+[Not right now]
-> goose_wait


= goose_wait
Goose of Lies: You're no fun... But come back if you change your mind. #avatars:goose
-> END


= goose_talk2
Player: Hey! You didn't say you'd hit me if I answered wrong! #avatars:player

Goose of Lies: I didn't? Whoopsie, must have slipped my mind, hehe. #avatars:goose
Goose of Lies: You still wanna try my riddle again? #avatars:goose
+[Fine, that reward better be worthwile]
-> goose_riddle

+[Hell no]
-> goose_wait


= goose_riddle
Goose of Lies: How many trees are on this island? #avatars:goose
+[1]
-> goose_correct

+[7]
-> goose_wrong

+[6]
-> goose_correct

+[420]
-> goose_correct



= goose_wrong
Goose of lies: Nope, wrong answer! Try again. #avatars:goose
~Event("take_damage1")
-> END

= goose_correct
Goose of Lies: Ding Ding Ding! Just the answer I was looking for! Here, as a reward I shall give you these seeds! #avatars:goose
~Event("give_seeds")
-> END


= goose_done
Goose of Lies: Shoo now human! You have already bested my riddle and recieved my prize, there is nothing left for you here. #avatars:goose
~Event("count_interaction")
-> END

= goose_secretLie
Goose of Lies: ... #avatars:goose
Goose of Lies: ........ #avatars:goose
Goose of Lies: ........................... ............................... #avatars:goose
Goose of Lies: Okay, I lied XD. Since you like wasting your time so much I shall reward your efforts and simply give you all the ingredients you need for your cake! How's that XD #avatars:goose
~Event("dropAll_ingredients")
-> END


= goose_trulyDone
Goose of lies: You're quite the greedy fella aren't you? Not that I mind; you should just know that now truly nothing is left for you here. #avatars:goose
-> END



= frog_talk
Frog of Truth: Good day traveler. Are you perhaps interested in taking part in my riddle? I must warn you though, if you are ill prepared you'll suffer consequenses if you give wrong answers. #avatars:frog
-> frog_riddle

+[Wait, let me prepare a bit more first]
-> frog_wait



= frog_wait
Frog of Truth: That's okay, I'll be here whenever you are ready. #avatars:frog
-> END



= frog_riddle
Frog of Truth: Well then if you are prepared here goes:: What animal can you find on this beach? #avatars:frog
+[Cow]
-> frog_correct1

+[Frog]
-> frog_incorrect

+[Goose]
-> frog_correct1

+[Duck]
-> frog_incorrect



= frog_correct1
Frog of Truth: A great answer! I see you were paying attention on your travels. On to the next question. #avatars:frog
-> frog_riddle2



= frog_riddle2
Frog of Truth: What is the name of the man in the mines? #avatars:frog
+[Jerry]
-> frog_incorrect

+[Michael]
-> frog_incorrect

+[Mike]
-> frog_correct2

+[Jürgen]
-> frog_incorrect


= frog_correct2
Frog of Truth: Very good! Now, final question, you almost got it! #avatars:frog 
-> frog_riddle3



= frog_riddle3
Frog of Truth: What is my name? #avatars:frog
+[Frog of Truth]
-> frog_name

+[Frog of Lies]
-> frog_name

+[Frog of Wisdom]
-> frog_name



= frog_name
Frog of Truth: Uhh well, technically my name is Jared, but you couldn't possibly have known that sooo... Good job on beating my riddles! Here's your reward. #avatars:frog
~Event("give_bee")
-> END


= frog_incorrect
Frog of Truth: That is unfortunately the wrong answer. Please try again. #avatars:frog
~Event("take_damage")
-> END


= frog_done
Frog of Truth: No more riddles here for you. See you around. #avatars:frog
-> END




= cow_talk
Cow of Wisdom: Good day to you traveler. Would you like me to bestow some wisdom upon you? #avatars:cow

+[Oh yes I love me some wisdom]
-> cow_wisdom

+[Nah I'm good I don't need wisdom I'm already very wise]
-> cow_noWisdom


= cow_wisdom
Cow of Wisdom: Well then here is my wisdom! Aside from me, there are two other animals outside here with me. If you manage to give them the answers they seek I shall reward you with something you need on your quest. #avatars:cow
~Event("set_interaction")
-> END


= cow_talk2
Cow of Wisdom: Are you having trouble with thr riddles of my two friends? Would you perhaps care for a bit more of my wisdom? #avatars:cow

+[Yes, please. Save me with your wisdom]
-> cow_wisdom2

+[No need I am smart cookie]
-> cow_noWisdom


= cow_wisdom2
Cow of Wisdom: Well then here is my wisdom! Have you paid close attention to the names of my two riddle giving friends? Perhaps this may help you in figuring out what kind of answer they both seek. #avatars:cow
-> END



= cow_noWisdom
Cow of Wisdom: Well then, if you change your mind I'll be right here. #avatars:cow
->END



= cow_solvedRiddles
Cow of Wisdom: Marvelous my friend! You are truly wise if you managed to solve those riddles! As promised I shall now give you your reward. #avatars:cow
~Event("give_milk")
-> END


= cow_done
Cow of Wisdom: I neither have more wisdom to bestow upon you, nor have I anything left to give you. Farewell my friend. #avatars:cow
-> END





= buba_giveSeeds
Buba: Hello there sweetie pie! Would you like to have some wheat seeds? #avatars:farmer_buba

Player: Uhhh sure..? No catch? You don't want me to get you anything first? #avatars:player

Buba: Nope, no catch. just felt like being generous today! You wan't em or not? #avatars:farmer_buba
*[Sure, I'll take them]
Buba: Alrighty! Here ya go, have a nice day! #avatars:farmer_buba
~Event("give_WheatSeeds")
-> END

*[Nahh, too suspicous]
Buba: Okay..? Whatever man, I'll just, leave them on the floor here then? #avatars:farmer_buba
~Event("give_evilSeeds")
-> END


= seeds_notPickedUp
Buba: Aren't you gonna pick them up? #avatars:farmer_buba
-> END


= buba_happy
Buba: Welcome back sweetie pie! Unfortunately I don't got any more seeds for you at the moment, but I hope you have a great rest of your day regardless! #avatars:farmer_buba
-> END


= buba_confused
Buba: (Is this what I get for trying to be nice for once?) Oh... you're back... please leave. #avatars:farmer_buba
-> END


= healing_bed
Would you like to take a nap?
+[yes]
Your HP was maxed out!
~Event("max_hp")
~Event("FMOD_StartHealing")
-> END

+[no]
-> END

