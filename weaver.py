from PIL import Image

# Returns a bool indicating whether a pixel represents a horizontal or vertical thread
def isHorizontal(c, r, ou):
    return (((c % (2 * ou)) - (r % (2 * ou))) + (ou - 1)) % (2 * ou) < ou

# Returns the expanded thread array so each thread has its own element
def getExpandedThreadArray(threadArray):
    expandedThreadArray = []
    for thread in threadArray:
        for i in range(thread[1]):
            expandedThreadArray.append(thread[0])
    return expandedThreadArray

# Returns an RGB tuple indicating the color a given pixel should be
def getPixelColor(c, r, ou, expandedThreadArray):  
    if isHorizontal(c, r, ou):
        var = r
    else:
        var = c
    return expandedThreadArray[var % len(expandedThreadArray)]

# Creates and saves an image representing a woven tapestry
def drawCanvas(cols, rows, threadArray, ou):
    im = Image.new('RGB', (cols, rows))
    expandedThreadArray = getExpandedThreadArray(threadArray)
    for c in range(cols):
        for r in range(rows):
            im.putpixel((c, r), getPixelColor(c, r, ou, expandedThreadArray))
    im.save('weaver.output.png')

# Testing code
if __name__=='__main__':
    threadArray = [[(128, 000, 000), 4], # 4 threads of dark red
                   [(128, 128, 128), 8], # 8 threads of gray
                   [(000, 000, 128), 4], # 4 threads of dark blue
                   [(128, 128, 128), 8]] # 8 threads of gray
    drawCanvas(256, 128, threadArray, 2) # Create 256 x 128 image. Over/under number is 2
