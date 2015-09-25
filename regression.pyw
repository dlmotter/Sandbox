# Daniel Motter
# Program to plot a regression line

from graphics import *
def main():
    done=True
    sumx,sumy,sumxy,sumx2,sumy2,n,m,b,r=0,0,0,0,0,0,0,0,0
    win=GraphWin("Linear Regression",500,500)
    win.setCoords(-10,-10,10,10)
    Line(Point(-10,0),Point(10,0)).draw(win)
    Line(Point(0,10),Point(0,-10)).draw(win)
    donerect=Rectangle(Point(-9,-8),Point(-7,-9))
    donerect.setWidth(2)
    donerect.draw(win)
    donetext=Text(Point(-8,-8.5),"Done")
    donetext.draw(win)
    while done:
        point=win.getMouse()
        point.draw(win)        
        x=point.getX()
        y=point.getY()
        if -9<x<-7 and -9<y<-8:
            point.undraw()
            done=False
        else:
            n+=1            
            sumx+=x
            sumy+=y
            sumxy+=(x*y)
            sumx2+=x**2
            sumy2+=y**2
    try:
        m=((n*sumxy)-(sumx*sumy))/((n*sumx2)-sumx**2)
        b=(sumy-(m*sumx))/n
        r=((n*sumxy)-(sumx*sumy))/((((n*sumx2)-(sumx**2))**0.5)*(((n*sumy2)-(sumy**2))**0.5))
        line1=Point(-10,((m*-10)+b))
        line2=Point(10,((m*10)+b))
        donerect.undraw()
        donetext.undraw()
        finalline=Line(line1,line2)
        finalline.setWidth(2)
        finalline.draw(win)
        if ((m*10)+b)>=0:
            text1,text2,text3=-5,-6,-8
        else:
            text1,text2,text3=7,6,4              
        Text(Point(3.5,text1),"y="+str(round(m,4))+"x+"+str(round(b,4))).draw(win)  
        Text(Point(5,text2),"Correlation Coefficient="+str(round(r,4))).draw(win)
        Text(Point(5,text3),"Click anywhere to close").draw(win)
    except ZeroDivisionError:
        errort=Text(Point(5,5),"You need at least two points.").draw(win)
        errort2=Text(Point(5,4),"Click anywhere to close.").draw(win)
    win.getMouse()
    win.close()
    
if __name__=="__main__":
    main()
    
