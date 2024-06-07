#pragma once
#include "ConvexShape.h"
class Triangle : public ConvexShape
{
public:
	Triangle(CVector3d const& A, CVector3d const& B, CVector3d const& C, CMatrix4d const& transform = CMatrix4d());
};
