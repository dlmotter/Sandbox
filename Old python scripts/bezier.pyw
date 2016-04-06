# Daniel Motter
# Program to input and draw a Bézier curve
# https://en.wikipedia.org/wiki/B%C3%A9zier_curve#General_definition

from graphics import *
def main():
    win=GraphWin("Bézier Curve",400,400)
    text=Text(Point(200,25),"Click points to make a Bézier Curve.")
    donetext=Text(Point(375,390),"Done")
    donerect=Rectangle(Point(350,380),Point(400,400))
    donerect.setWidth(2)
    done=True
    xlist,ylist,n=[],[],0
    text.draw(win)
    donetext.draw(win)
    donerect.draw(win)
    while done:
        pt=win.getMouse()
        xpt=pt.getX()
        ypt=pt.getY()
        if 350<xpt<400 and 380<ypt<400:
            donetext.undraw()
            donerect.undraw()
            done=False
        else:
            pt.draw(win)
            n+=1
            xlist.append(xpt)
            ylist.append(ypt)
            if n>1:
                line=Line(Point(xlist[n-2],ylist[n-2]),Point(xlist[n-1],ylist[n-1]))
                line.setFill("gray")
                line.draw(win)
    b=[0]*(n+1)
    b[0]=1
    for i in range(1,n):
        b[i]=1
        j=i-1
        while j>0:
            b[j]+=b[j-1]
            j-=1               
    for p in range(1000):
        t=p/1000
        x,y=0,0
        for i in range(n):
            x+=(b[i])*((1-t)**((n-1)-i))*(t**i)*xlist[i]
            y+=(b[i])*((1-t)**((n-1)-i))*(t**i)*ylist[i]
        Point(x,y).draw(win)   
    text.undraw()
    Text(Point(200,25),"Click anywhere to close.").draw(win)
    win.getMouse()
    win.close()
if __name__=="__main__":
    main()

