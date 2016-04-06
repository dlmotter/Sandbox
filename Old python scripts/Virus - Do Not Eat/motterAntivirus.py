import os

def infected(filename):
    f = open(filename, 'r')
    infected = '## "#@! infected by virus !@#"' in f.read()
    f.close()
    return infected

def scan():
    files = os.listdir(os.getcwd())
    [print(file) for file in files if infected(file)]

def removeVirus(filename):
    originalCode = ''
    partOfOriginalCode = True
    with open(filename, 'r') as this:
        for line in this:
            if line == '## "#@! infected by virus !@#"\n':
                partOfOriginalCode = False
            if partOfOriginalCode:
                originalCode += line
    this.close()
    f = open(filename, 'w')
    f.write(originalCode)
    f.close()
