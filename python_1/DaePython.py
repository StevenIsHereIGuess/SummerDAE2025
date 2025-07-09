import random

print("Welcome!")

secret_number = random.randint(1, 10)

while True:                                      
    guess = input("Guess a number between 1 and 10: ")

    # Turn text into a number (and reject bad input)
    try:
        guess = int(guess)
    except ValueError:
        print("That's not a valid number. Try again.")
        continue                                 

    if guess == secret_number:
        print("Correct! You win!")
        break                                  
    else:
        print("Wrong! Try again.")