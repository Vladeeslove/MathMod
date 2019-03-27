#pragma once
#include <math.h>
#define M_PI       3.14159265358979323846

class Ball
{

public:
	float x,				//X by center
		y,					//Y by center
		speed_x,			//update X on interation
		speed_y,			//update Y on interation
		radius,
		colorR,
		colorG,
		colorB;
	Ball(const Ball& _a)
	{
		x = _a.x;
		y = _a.y;
		speed_x = _a.speed_x;
		speed_y = _a.speed_y;
		radius = _a.radius;
		colorR = _a.colorR;
		colorG = _a.colorG;
		colorB = _a.colorB;
	}
	Ball(float x, float y, float speed_x, float speed_y, float radius, float colorR, float colorG, float colorB)
		:x(x), y(y), speed_x(speed_x), speed_y(speed_y), radius(radius), colorR(colorR), colorG(colorG), colorB(colorB)
	{
	}//init data this class
	~Ball() 
	{
	}

};

float direction(float angle, Ball iCircle, Ball jCircle)			//function for define direction   (use -angle or +angle)
{
	float AB,					//AB,BC,CA - sides of triangle
		BC,
		CA,
		x3 = jCircle.x,		//x3,y3 temporarily rotate a center j-circle
		y3 = jCircle.y,
		S1, S2;					//for the received areas

	x3 = iCircle.x + (jCircle.x - iCircle.x) * cos(angle) - (jCircle.y - iCircle.y) * sin(angle);//Rotate cordinate j-circle in relation to current circle
	y3 = iCircle.y + (jCircle.y - iCircle.y) * cos(angle) + (jCircle.x - iCircle.x) * sin(angle);


	AB = sqrt(pow((iCircle.x - x3), 2) + pow((iCircle.y - y3), 2));													//distance between centers of two circles
	CA = sqrt(pow(iCircle.speed_x, 2) + pow(iCircle.speed_y, 2));													//distance between 'i-center' current circle && 'i-center + one step'
	BC = sqrt(pow((x3 - (iCircle.x + iCircle.speed_x)), 2) + pow((y3 - (iCircle.y + iCircle.speed_y)), 2));		//distance between 'i-center + one step' && 'j-center'
	S1 = 0.5f*(CA*AB*BC);																								//Area by ABC triangle


	angle = angle * -1.0f;																								//reverse direction...

	x3 = iCircle.x + (jCircle.x - iCircle.x) * cos(angle) - (jCircle.y - iCircle.y) * sin(angle);
	y3 = iCircle.y + (jCircle.y - iCircle.y) * cos(angle) + (jCircle.x - iCircle.x) * sin(angle);
	AB = sqrt(pow((iCircle.x - x3), 2) + pow((iCircle.y - y3), 2));
	CA = sqrt(pow(iCircle.speed_x, 2) + pow(iCircle.speed_y, 2));
	BC = sqrt(pow((x3 - (iCircle.x + iCircle.speed_x)), 2) + pow((y3 - (iCircle.y + iCircle.speed_y)), 2));
	S2 = 0.5f*(CA*AB*BC);

	if (S1 > S2)//if default direction (+angle) have larger area, we will direct in the reverse direction (-angle)
	{
		return -1.0f;
	}
	else return 1.0f;
}

void CollitionOfWall(std::vector<Ball> &arrBalls, float LongX, float LongY)
{
	for (int i = 0; i < arrBalls.size(); i++)
	{
		if (arrBalls.at(i).x < LongX*(-1.0f) + arrBalls.at(i).radius || arrBalls.at(i).x > LongX - arrBalls.at(i).radius)			//left frame collision
			arrBalls.at(i).speed_x = arrBalls.at(i).speed_x *(-1.0f);																//right frame collision

		if (arrBalls.at(i).y > LongY - arrBalls.at(i).radius || arrBalls.at(i).y < LongY*(-1.0f) + arrBalls.at(i).radius)			//top frame collision
			arrBalls.at(i).speed_y = arrBalls.at(i).speed_y *(-1.0f);																//bottom frame collision
	}

}


float ABC_triagle(Ball iCircle, Ball jCircle)
{
	float AC, AB, BC;				//ABC triangle
	AC = sqrt(pow((iCircle.x - (iCircle.x + iCircle.speed_x)), 2) + pow((iCircle.y - (iCircle.y + iCircle.speed_y)), 2));

	AB = sqrt(pow((iCircle.x - jCircle.x), 2) + pow((iCircle.y - jCircle.y), 2));

	BC = sqrt(pow((jCircle.x - (iCircle.x + iCircle.speed_x)), 2) + pow((jCircle.y - (iCircle.y + iCircle.speed_y)), 2));
	return (pow(AC, 2) + pow(AB, 2) - pow(BC, 2)) / (2.0*AC*AB);
}


void CollitionOfCircle(Ball &iCircle, Ball &jCircle)
{
	float cosAlpha,					//for get cos(angle_collition) from ABC triangle
		pX,							//temp X for new center cordinate 
		pY,							//temp Y for new center cordinate 
		angle;
	float arcc = 0;


	cosAlpha = ABC_triagle(iCircle, jCircle);//(pow(AC, 2) + pow(AB, 2) - pow(BC, 2)) / (2.0*AC*AB);				//get angle collition

	if (cosAlpha < -1.0)cosAlpha = -1;
	if (cosAlpha > 1.0)cosAlpha = 1;
	arcc = acos(cosAlpha)*180.0 / M_PI;												//convert to degree


	if (arcc < 90.0)//only for sharp angle
	{
		angle = 180.0 - arcc*2; /*now we have a collition angle, here i add second angle
									and get angle need for rotate current 'i-circle'*/
		arcc *= (M_PI / 180.0);		//convert to radian

		angle *= direction(arcc, iCircle, jCircle);//switch direction
		angle *= (M_PI / 180.0);
	}
	else
	{
		angle = arcc;
		arcc *= (M_PI / 180.0);
		//no phisics
		if (direction(arcc, iCircle, jCircle) == 1)
		{
			angle = 180.0 - angle;
		}
		else
		{
			angle = 180.0 + angle;
			//angle *= -1.0;
		}
		angle *= (M_PI / 180.0);
	}

	pX = (iCircle.speed_x*cos(angle) - iCircle.speed_y*sin(angle));	//rotate speed_x 
	pY = (iCircle.speed_y*cos(angle) + iCircle.speed_x*sin(angle));	//and speed_y
	iCircle.speed_x = pX;
	iCircle.speed_y = pY;
}
