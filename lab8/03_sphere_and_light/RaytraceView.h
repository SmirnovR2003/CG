// 01_raytrace_baseView.h : interface of the CMy01_raytrace_baseView class
//
/////////////////////////////////////////////////////////////////////////////

#pragma once

#include "Renderer.h"
#include "RenderContext.h"
#include "Scene.h"
#include "Plane.h"
#include "CheckerShader.h"
#include "Sphere.h"
#include "Cube.h"
#include "SimpleDiffuseShader.h"
#include "ConvexShape.h"
#include "Triangle.h"
#include "Tetrahedron.h"
#include "Octahedron.h"
#include "Icosahedron.h"

class CFrameBuffer;

class CRaytraceView : public CWindowImpl<CRaytraceView>
{
	enum
	{
		FRAMEBUFFER_UPDATE_TIMER = 1
	};
public:
	CRaytraceView();
	~CRaytraceView();
	DECLARE_WND_CLASS(NULL)

	BOOL PreTranslateMessage(MSG* pMsg);

	BEGIN_MSG_MAP(CRaytraceView)
		MESSAGE_HANDLER(WM_PAINT, OnPaint)
		MESSAGE_HANDLER(WM_TIMER, OnTimer)
		MESSAGE_HANDLER(WM_CREATE, OnCreate)
		MESSAGE_HANDLER(WM_ERASEBKGND, OnEraseBkgnd)
	END_MSG_MAP()

// Handler prototypes (uncomment arguments if needed):
//	LRESULT MessageHandler(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
//	LRESULT CommandHandler(WORD /*wNotifyCode*/, WORD /*wID*/, HWND /*hWndCtl*/, BOOL& /*bHandled*/)
//	LRESULT NotifyHandler(int /*idCtrl*/, LPNMHDR /*pnmh*/, BOOL& /*bHandled*/)
private:
	LRESULT OnCreate(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
	LRESULT OnPaint(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
	LRESULT OnTimer(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
	LRESULT OnEraseBkgnd(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);

	// Отрисовка содержимого буфера кадра на контексте устройства
	void DrawFrameBuffer(CDC & dc, int x, int y);
	bool UpdateFrameBuffer();
private:
	CRenderContext m_context;
	CRenderer m_renderer;
	CScene	m_scene;

	std::unique_ptr<CFrameBuffer> m_pFrameBuffer;

	// Геометрические объекты, присутствующие в сцене
	CPlane m_plane;
	CSphere m_sphere1;
	CSphere m_sphere2;
	Cube m_cube;
	ConvexShape m_convexShape1;
	Triangle m_triangle1;
	Tetrahedron m_tetrahedron1;
	Octahedron m_octahedron1;
	Icosahedron m_icosahedron1;

	// Шейдеры
	CSimpleShader m_simpleDiffuseShader1;
	CSimpleShader m_simpleDiffuseShader2;
	CSimpleShader m_simpleDiffuseShader3;
	CSimpleShader m_simpleDiffuseShader4;
	CSimpleShader m_simpleDiffuseShader5;
	CSimpleShader m_simpleDiffuseShader6;
	CSimpleShader m_simpleDiffuseShader7;
	CSimpleShader m_simpleDiffuseShader8;
	CCheckerShader m_checkerShader;
};
