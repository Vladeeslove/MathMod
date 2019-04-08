var canvas=document.getElementById('MyCanvas');
var ctx=canvas.getContext('2d');

function Ball(pX, pY, pSpeedX, pSpeedY,pRadius,pColor)
{
	this.X=pX;
	this.Y=pY;
	this.SpeedX=pSpeedX;
	this.SpeedY=pSpeedY;
	this.Radius=pRadius;
	this.colorCircle= pColor;
}

function MyRandom(n, m)
{
	var outRandom;
	do
	{
		outRandom=(Math.floor(Math.random( ) * (n - m + 1)) + m)
	}while(outRandom == 3);
	return outRandom;
}

//init objects
var MyArrayBalls=new Array( 
	new Ball(150,150,MyRandom(0,6)-3,MyRandom(0,6)-3,MyRandom(25,100),"rgb("+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+")"),
	new Ball(150,550,MyRandom(0,6)-3,MyRandom(0,6)-3,MyRandom(25,100),"rgb("+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+")"),
	new Ball(750,150,MyRandom(0,6)-3,MyRandom(0,6)-3,MyRandom(25,100),"rgb("+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+")"),
	new Ball(450,350,MyRandom(0,6)-3,MyRandom(0,6)-3,MyRandom(25,100),"rgb("+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+")"),
	new Ball(750,550,MyRandom(0,6)-3,MyRandom(0,6)-3,MyRandom(25,100),"rgb("+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+", "+(MyRandom(0,100)%100)+")")
	);

function CollitionOfWall(i)
{

	if(MyArrayBalls[i].X > canvas.width - MyArrayBalls[i].Radius || MyArrayBalls[i].X < 0 + MyArrayBalls[i].Radius)
	{
		MyArrayBalls[i].SpeedX*=-1.0;
	}
	if(MyArrayBalls[i].Y > canvas.height - MyArrayBalls[i].Radius || MyArrayBalls[i].Y < 0 + MyArrayBalls[i].Radius)
	{
		MyArrayBalls[i].SpeedY*=-1.0;
	}
}

function AdjustDirection(angle, i, j)
{
	var 
	AB,
	BC,
	CA,
	tempXj=MyArrayBalls[j].X,
	tempYj=MyArrayBalls[j].Y,
	S1,S2;

	tempXj = MyArrayBalls[i].X + (MyArrayBalls[j].X - MyArrayBalls[i].X) * Math.cos(angle) - (MyArrayBalls[j].Y - MyArrayBalls[i].Y) * Math.sin(angle);
	tempYj = MyArrayBalls[i].Y + (MyArrayBalls[j].Y - MyArrayBalls[i].Y) * Math.cos(angle) + (MyArrayBalls[j].X - MyArrayBalls[i].X) * Math.sin(angle);

	AB = Math.sqrt(Math.pow((MyArrayBalls[i].X - tempXj), 2) + Math.pow((MyArrayBalls[i].Y - tempYj), 2));														//distance between centers of two circles
	CA = Math.sqrt(Math.pow(MyArrayBalls[i].SpeedX, 2) + Math.pow(MyArrayBalls[i].SpeedY, 2));																	//distance between 'i-center' current circle && 'i-center + one step'
	BC = Math.sqrt(Math.pow((tempXj - (MyArrayBalls[i].X + MyArrayBalls[i].SpeedX)), 2) + Math.pow((tempYj - (MyArrayBalls[i].Y + MyArrayBalls[i].SpeedY)), 2));//distance between 'i-center + one step' && 'j-center'
	S1 = 0.5*(CA*AB*BC);																																		//Area by ABC triangle

	angle = angle * -1.0;	

	tempXj = MyArrayBalls[i].X + (MyArrayBalls[j].X - MyArrayBalls[i].X) * Math.cos(angle) - (MyArrayBalls[j].Y - MyArrayBalls[i].Y) * Math.sin(angle);
	tempYj = MyArrayBalls[i].Y + (MyArrayBalls[j].Y - MyArrayBalls[i].Y) * Math.cos(angle) + (MyArrayBalls[j].X - MyArrayBalls[i].X) * Math.sin(angle);

	AB = Math.sqrt(Math.pow((MyArrayBalls[i].X - tempXj), 2) + Math.pow((MyArrayBalls[i].Y - tempYj), 2));														
	CA = Math.sqrt(Math.pow(MyArrayBalls[i].SpeedX, 2) + Math.pow(MyArrayBalls[i].SpeedY, 2));																	
	BC = Math.sqrt(Math.pow((tempXj - (MyArrayBalls[i].X + MyArrayBalls[i].SpeedX)), 2) + Math.pow((tempYj - (MyArrayBalls[i].Y + MyArrayBalls[i].SpeedY)), 2));
	S2 = 0.5*(CA*AB*BC);	

	if(S1 > S2)
	{
		return -1;
	}
	else return 1;
}

function ABCTriagle(i,j)
{
	var 
	AC,
	AB,
	BC;
	AC = Math.sqrt(Math.pow((MyArrayBalls[i].X - (MyArrayBalls[i].X + MyArrayBalls[i].SpeedX)), 2) + Math.pow((MyArrayBalls[i].Y - (MyArrayBalls[i].Y + MyArrayBalls[i].SpeedY)), 2));

	AB = Math.sqrt(Math.pow((MyArrayBalls[i].X - MyArrayBalls[j].X), 2) + Math.pow((MyArrayBalls[i].Y - MyArrayBalls[j].Y), 2));

	BC = Math.sqrt(Math.pow((MyArrayBalls[j].X - (MyArrayBalls[i].X + MyArrayBalls[i].SpeedX)), 2) + Math.pow((MyArrayBalls[j].Y - (MyArrayBalls[i].Y + MyArrayBalls[i].SpeedY)), 2));

	return ((Math.pow(AC,2) + Math.pow(AB,2) - Math.pow(BC,2)) / (2.0 * AC * AB));
}

function CollitionOfCircles(i,j)
{
	var cosAlpha,
	tempX,
	tempY,
	angle,
	arcc=0.0;
	
	cosAlpha=ABCTriagle(i,j);
	if(cosAlpha < -1.0) cosAlpha = -1.0;
	if(cosAlpha > 1.0) cosAlpha = 1.0;

	arcc = Math.acos(cosAlpha)*180.0/Math.PI;

	if(arcc < 90.0)
	{
		angle = 180.0 - arcc * 2.0;

		arcc *= Math.PI / 180.0;

		angle *= AdjustDirection(arcc, i, j);

		angle *= Math.PI / 180.0;
	}
	else
	{
		angle = arcc;
		arcc *= Math.PI / 180.0;

		//no phisics
		if(AdjustDirection(arcc, i, j) == 1)
		{
			angle = 180.0 - angle;
		}
		else
		{
			angle = 180.0 + angle;
		}
		angle *= Math.PI / 180.0;
	}

	tempX = (MyArrayBalls[i].SpeedX * Math.cos(angle) - MyArrayBalls[i].SpeedY * Math.sin(angle));	//rotate speed_x
	tempY =  (MyArrayBalls[i].SpeedY * Math.cos(angle) + MyArrayBalls[i].SpeedX * Math.sin(angle));	//and speed_y

	MyArrayBalls[i].SpeedX=tempX;
	MyArrayBalls[i].SpeedY=tempY;
}


function drawCircle(i)
{
	

		MyArrayBalls[i].X+=MyArrayBalls[i].SpeedX;
		MyArrayBalls[i].Y+=MyArrayBalls[i].SpeedY;
		ctx.beginPath();
		ctx.fillStyle = MyArrayBalls[i].colorCircle;
		
		ctx.arc(MyArrayBalls[i].X, MyArrayBalls[i].Y, MyArrayBalls[i].Radius, 0, 2 * Math.PI, true);
		ctx.fill();
	
}



function Run(){
ctx.clearRect(0, 0, canvas.width, canvas.height);

	for(var i=0;i<MyArrayBalls.length;i++)
	{

	CollitionOfWall(i);

		for(var j=0;j<MyArrayBalls.length;j++)
		{
			if(i!=j)
			{
				var CD=Math.sqrt(Math.pow((MyArrayBalls[i].X- MyArrayBalls[j].X),2)+Math.pow((MyArrayBalls[i].Y - MyArrayBalls[j].Y),2))-MyArrayBalls[i].Radius- MyArrayBalls[j].Radius;
				if(CD<0)
				{
				CollitionOfCircles(i,j);
				}
			}
		}
	}

	for(var i = 0; i<MyArrayBalls.length;i++)
	{
		drawCircle(i);
	}
	
}
setInterval(Run,10);