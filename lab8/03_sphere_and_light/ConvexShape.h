#pragma once
#include "Plane.h"
#include<vector>
class ConvexShape : public CPlane
{
public:
	/*
	��������� �������� �������������� ��������� ��������� ax+by+cz+d=0,
	� ����� �������� ���������� �������������� ���������
	*/
	ConvexShape(double a, double b, double c, double d, std::vector<CVector3d> const& points, CMatrix4d const& transform = CMatrix4d());

	/*
	���������� ����� ����������� ���� � ����������
	*/
	virtual bool Hit(CRay const& ray, CIntersection& intersection) const;

protected:

	ConvexShape(CMatrix4d const& transform = CMatrix4d());
	void SetPlaneAndPonts(double a, double b, double c, double d, std::vector<CVector3d> const& points);

	virtual void OnUpdateTransform();

private:
	std::vector<CVector3d> m_points;
};
