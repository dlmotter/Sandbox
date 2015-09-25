def quicksort(instr):
    if len(instr) == 0:
        return instr
    else:
        small, large = '', ''
        for i in range(1, len(instr)):
            if ord(instr[i].lower()) < ord(instr[0].lower()):
                small += instr[i]
            else:
                large += instr[i]
        return quicksort(small) + instr[0] + quicksort(large)

if __name__ == '__main__':
    print(quicksort(input('Input string: ')))
