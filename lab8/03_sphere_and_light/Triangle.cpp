#include "stdafx.h"
#include "Triangle.h"
#include "VectorMath.h"
#include <iostream>

Triangle::Triangle(CVector3d const& A, CVector3d const& B, CVector3d const& C, CMatrix4d const& transform)
	: ConvexShape(transform)
{
	CVector3d AB = B - A;
	CVector3d AC = C - A;
	CVector3d normal = Normalize(Cross(AB, AC));

	float a = normal.x;
	float b = normal.y;
	float c = normal.z;

	float d = -Dot(normal, A);

	ConvexShape::SetPlaneAndPonts(a, b, c, d, { A, B, C });

}
