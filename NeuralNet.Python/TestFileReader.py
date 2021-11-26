from os import write
from GenerateData import read_all_scripts, generate_chars

file = read_all_scripts()
open("./Data/Combined.txt","w").write(file)