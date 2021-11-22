from GenerateData import read_all_scripts, generate_chars

file = read_all_scripts()
chars = generate_chars(file)

print(chars)