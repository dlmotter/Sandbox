def char_count(instr):
    values = {}
    outstr = ''
    for i in range(len(instr)):
        if instr[i] in values:
            values[instr[i]] += 1
        else:
            values[instr[i]] = 1
    for key in sorted(values):
        outstr += key + ': ' + str(values[key]) + ', '
    outstr = outstr[:-2]
    return outstr

if __name__ == '__main__':
    print(char_count(input('Input string: ')))
