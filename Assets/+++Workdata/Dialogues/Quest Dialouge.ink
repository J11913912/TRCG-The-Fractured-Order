=== quest ===

= Mining_Ores
Mike: Hello there! Very glad to see you lass, ya'see, I'm embarrassed to say, but I sprained my ankle not too long ago during my shift, but I still got all these ores to mine in here or my boss'll kill me... you think you could help me out by mining them for me? #avatars:miner_mike

*[Sure thing boss!]
Mike: Thanks lass yer a lifesaver! Here, take my pickaxe, you'll need it to mine them ores. Just bring em ere when yer done! #avatars:miner_mike
~Event("oreQuest_start")
~Event("give_pickaxe")
-> END

+[Sorry, not right now...]
Mike: That's alright lass. I'll still be here for a while if you change your mind though, would greatly appreciate it! #avatars:miner_mike
-> END

= Quest_Ongoing
Mike: How's mining them ores coming along? #avatars:miner_mike
-> END

= Quest_Completed
Mike: Hey lass! Thanks for mining those ores for me! Yer a real life saver! Here, I got a little sum for you, hope it'll be of help to you! #avatars:miner_mike
~Event("give_sword")
~Add_State("item_ore_blue", -1)
~Add_State("item_ore_red", -1)
~Add_State("item_ore_purple", -1)
-> END

= After_Quest
Mike: Hey Lass! Thanks again for helping me with those ores! #avatars:miner_mike
-> END




= Bob_getKey
Bob: Hey there fella! It seems you'd like to use this patch of Farmland here. I'm sorry to dissapoint you, however I seem to have lost the key. If you find it for me, I'll gladly unlock the gate for you! #avatars:farmer_bob
*[Accept]
~ Event("keyQuest_start")
-> END

+[Decline]
Bob: Okay, but if you wanna go through this gate you gotta bring me that key! #avatars:farmer_bob
-> END

= Bob_sucess
Bob: Hey! You managed to find the key, thank you so much! Here, as promised I'll unlock the gate for you! #avatars:farmer_bob
~ Event("unlock_gate")
~ Add_State("item_key", -1)
-> END

= Bob_happy
Bob: Hey! Thanks again for bringing me that key! #avatars:farmer_bob
-> END

= Bob_reminder
Bob: Did you forget what you were supposed to do? #avatars:farmer_bob
Bob: No problem! Let me remind you:: I need you to please bring me the key to this gate so I may unlock it for you. Hope this helps! #avatars:farmer_bob
-> END

= Bob_confusion
Bob: Um... Is it really that hard to find the key? I could'nt have dropped it far... #avatars:farmer_bob
-> END

= Bob_hint
Bob: You know what, I think I actually see the key right over there on the ground! It's very hard to miss, nows your chance to go grab it! #avatars:farmer_bob
-> END

= Bob_hint2
Bob: The key is literally right behind you on the floor. Are your eyes okay? Are you not even trying? #avatars:farmer_bob
-> END

= Bob_frustration
Bob: ... #avatars:farmer_bob
Bob: ......... #avatars:farmer_bob
Bob: You gotta be fucking with me, aren't you? #avatars:farmer_bob
-> END

= Bob_giveUp
Bob: Oh my fucking god. You know what, fine you win. You're not even trying to get the key! Here, just go through the gate and then leave. #avatars:farmer_bob
~ Event("unlock_gate")
-> END

= Bob_angry
Bob: You again? I don't wanna see you. Get out of my sight. #avatars:farmer_bob
-> END


= shop_vendor
Vendor: Good day missy! Are you perhaps interested in this hoe I have here? it may be of good use to you if you plan on growing and harvesting anything hehe. #avatars:shop_vendor
Vendor: You see, it is simply way too hot today and I do not wish to leave this nice little spot of shade, yet I still wish to enjoy a bit of nature. Tell you what, I'll give the hoe to you, if you bring me 8 flowers. #avatars:shop_vendor

*[Alrighty you got it!]
Vendor: Thank you! See you in a bit. #avatars:shop_vendor
-> END

= collecting_flowers
Vendor: Welcome back! I see you only got FlowerCounter flowers so far, remember to bring me all 8! #avatars:shop_vendor
-> END

= finish_collecting
Vendor: That's all 8 flowers! Thank you for bringing them to me, you really made my day! Here's the hoe, as promised. #avatars:shop_vendor
~Event("give_hoe")
-> END

= after_quest
Vendor: Hello again. I unfortunately don't got any more stuff to give to you, but thank you again for those flowers! #avatars:shop_vendor
-> END



= chicken1_wantSeeds
Player: Hello there! Do you mayhaps have an egg for me? #avatars:player

Chicken1: Hmm maybe. But I won't give it to you for free. Bring me some seeds and I may consider it. #avatars:chicken2_white
-> END

= chicken1_wantSeeds2
Chicken1: No seeds no egg that was the deal. #avatars:chicken2_white
-> END

= chicken1_giveSeeds
player: Here are your seeds as promised! May I have an egg now? #avatars:player

Chicken1: Hmm yes, a deal is a deal I suppose. Just grab it from my nest here and leave. #avatars:chicken2_white
~Event("unlock_nest1")
~Add_State("item_seed", -3)
-> END

= chicken1_done
Chicken1: What are you still doing here? We're done now aren't we? We both got what we wanted. #avatars:chicken2_white
-> END


= chicken3_wantBee
Player: Hi! May I have an egg? #avatars:player

Chicken3: Hmmmm sure, but only if you bring me a bee. #avatars:chicken3_black

Player: You got it! #avatars:player
-> END


= chicken3_wantBee2
Chicken3: Still waiting for that bee. #avatars:chicken3_black
-> END

= chicken3_giveBee
Player: Here, one bee for you! #avatars:player

Chicken3: Thanks, and over there you can grab your egg. #avatars:chicken3_black
~Event("unlock_nest2")
~Add_State("item_bee", -1)
-> END

= chicken3_done
Chicken3: Got no more eggs for you, leave me with my bee. #avatars:chicken3_black
-> END


