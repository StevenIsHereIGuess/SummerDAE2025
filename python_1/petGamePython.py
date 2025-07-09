import random
print ("Yellow World")

pet_name = input("What would you like to name your pet?")
print(f"Say hellow to your new pet, {pet_name}!")

starting_states = ["hungry", "sad", "dirty"]
state = random.choice(starting_states)

print (f"{pet_name} is {state}")
print (f"Do you want to play, feed, or clean {pet_name}")

while True:
    action = input("What do you do? [feed, play, clean, quit] >").strip().lower()

    if action == "quit":
        print("Goodbye!")
        break

    if state == "hungry":
        if action == "feed":
            state = "sad"
            print(f"😊 {pet_name} ate a snack but now looks a little lonely.")
        else:
            print("That didn’t help. Try feeding!")

    elif state == "sad":
        if action == "play":
            state = "dirty"
            print(f"🎾 {pet_name} had fun and rolled in the mud!")
        else:
            print("Your pet is sad. Maybe play with them?")

    elif state == "dirty":
        if action == "clean":
            state = "hungry"
            print(f"🛁 All clean! {pet_name}'s tummy growls…")
        else:
            print(f"Ew, {pet_name} STINKS!")

    print(f"{pet_name} is now {state}.")