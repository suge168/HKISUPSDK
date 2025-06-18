#include "stdafx.h"

#include "Convert.h"

#include <string>


BOOL A2UTF8(const char* pCont, const DWORD dwInSize, char* pOut, DWORD dwOutSize, DWORD* pOutLen)
{
    if (pCont == NULL || dwInSize == 0)
    {
        if (pOutLen != NULL)
        {
            *pOutLen = (DWORD)0;
        }
        return TRUE;
    }

    if (pOut == NULL)
    {
        return FALSE;
    }

    char* pIn = new char[dwInSize + 1];
    if (pIn == NULL)
    {
        return FALSE;
    }
    memset(pIn, '\0', dwInSize + 1);
    memcpy(pIn, pCont, dwInSize);

    if (strlen(pIn) == 0)
    {
        delete[] pIn;
        pIn = NULL;
        if (pOutLen != NULL)
        {
            *pOutLen = (DWORD)0;
        }
        return TRUE;
    }

    int iNum = MultiByteToWideChar(CP_ACP, NULL, pIn, -1, NULL, NULL);
    /*lint -e{119} û�ж������*/
    wchar_t* pBuffw = new(std::nothrow) wchar_t[(DWORD)iNum];
    if (pBuffw == NULL)
    {
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    if (MultiByteToWideChar(CP_ACP, NULL, pIn, -1, pBuffw, iNum) <= 0)
    {
        //MultiByteToWideCharִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    int iLen = WideCharToMultiByte(CP_UTF8, 0, pBuffw, iNum - 1, NULL, NULL, NULL, NULL);

    if (iLen <= 0)
    {
        //MultiByteToWideCharִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    if ((DWORD)(iLen) > dwOutSize)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }
    /*lint -e{119} û�ж������*/
    char* pLpsz = new char[(DWORD)iLen];
    if (pLpsz == NULL)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    if (WideCharToMultiByte(CP_UTF8, 0, pBuffw, iNum - 1, pLpsz, iLen, NULL, NULL) <= 0)
    {
        //WideCharToMultiByteִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pLpsz;
        pLpsz  = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    memset(pOut, 0, dwOutSize);
    memcpy(pOut, pLpsz, (DWORD)iLen);

    if (pOutLen != NULL)
    {
        *pOutLen = (DWORD)iLen;
    }

    delete[] pBuffw;
    pBuffw = NULL;
    delete[] pLpsz;
    pLpsz  = NULL;
    delete[] pIn;
    pIn = NULL;

    return TRUE;
}


/*********************************************************
������:	UTF82A
��������:	UTF8ת���ֽ�
�������:	cont - UTF8�����ַ���                       
�������:	
����ֵ:	    ���ֽ��ַ���
**********************************************************/
BOOL UTF82A(const char* pCont, const DWORD dwInSize, char* pOut, DWORD dwOutSize, DWORD* pOutLen)
{
    if (pCont == NULL || dwInSize == 0)
    {
        if (pOutLen != NULL)
        {
            *pOutLen = (DWORD)0;
        }
        return TRUE;
    }

    if (pOut == NULL)
    {
        return FALSE;
    }

    char* pIn = new char[dwInSize + 1];
    if (pIn == NULL)
    {
        return FALSE;
    }
    memset(pIn, '\0', dwInSize + 1);
    memcpy(pIn, pCont, dwInSize);

    if (strlen(pIn) == 0)
    {
        delete[] pIn;
        pIn = NULL;
        if (pOutLen != NULL)
        {
            *pOutLen = (DWORD)0;
        }
        return TRUE;
    }

    int iNum = MultiByteToWideChar(CP_UTF8, NULL, pIn, -1, NULL, NULL);
    /*lint -e{119} û�ж������*/
    wchar_t* pBuffw = new(std::nothrow) wchar_t[(DWORD)iNum];
    if (pBuffw == NULL)
    {
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    if (MultiByteToWideChar(CP_UTF8, NULL, pIn, -1, pBuffw, iNum) <= 0)
    {
        //MultiByteToWideCharִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    int iLen = WideCharToMultiByte(CP_ACP, 0, pBuffw, iNum - 1, NULL, NULL, NULL, NULL);

    if (iLen <= 0)
    {
        //WideCharToMultiByteִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    if ((DWORD)(iLen) > dwOutSize)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    /*lint -e{119} û�ж������*/
    char* pLpsz = new char[(DWORD)iLen];
    if (pLpsz == NULL)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    if (WideCharToMultiByte(CP_ACP, 0, pBuffw, iNum - 1, pLpsz, iLen, NULL, NULL) <= 0)
    {
        //WideCharToMultiByteִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pLpsz;
        pLpsz = NULL;
        delete[] pIn;
        pIn = NULL;
        return FALSE;
    }

    memcpy(pOut, pLpsz, (DWORD)iLen);
    if (pOutLen != NULL)
    {
        *pOutLen = (DWORD)iLen;
    }

    delete[] pBuffw;
    pBuffw = NULL;
    delete[] pLpsz;
    pLpsz = NULL;
    delete[] pIn;
    pIn = NULL;

    return TRUE;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/*********************************************************
������:	A2UTF8
��������:	���ֽ�תUTF8
�������:	pCont - GBK�����ַ�����pOut-UTF8�ַ�����dwOutSize-�����������С��pOutLen-ת������
�������:
����ֵ:	    UTF8
**********************************************************/
BOOL A2UTF8(const char* pCont, char* pOut, DWORD dwOutSize, DWORD* pOutLen)
{
    if (pCont == NULL)
    {
        if (pOutLen != NULL)
        {
            *pOutLen = (DWORD)0;
        }
        return TRUE;
    }

    if (pOut == NULL)
    {
        return FALSE;
    }

    int iNum = MultiByteToWideChar(CP_ACP, NULL, pCont, dwOutSize, NULL, NULL);
    /*lint -e{119} û�ж������*/
    wchar_t* pBuffw = new(std::nothrow) wchar_t[(DWORD)iNum];
    if (pBuffw == NULL)
    {
        return FALSE;
    }

    if (MultiByteToWideChar(CP_ACP, NULL, pCont, -1, pBuffw, iNum) <= 0)
    {
        //MultiByteToWideCharִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        return FALSE;
    }

    int iLen = WideCharToMultiByte(CP_UTF8, 0, pBuffw, -1, NULL, NULL, NULL, NULL);

    if (iLen <= 0)
    {
        //MultiByteToWideCharִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        return FALSE;
    }

    //if ((DWORD)(iLen) > dwOutSize)
    //{
    //    delete[] pBuffw;
    //    pBuffw = NULL;
    //    return FALSE;
    //}
    /*lint -e{119} û�ж������*/
    char* pLpsz = new char[(DWORD)iLen];
    if (pLpsz == NULL)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        return FALSE;
    }

    if (WideCharToMultiByte(CP_UTF8, 0, pBuffw, -1, pLpsz, iLen, NULL, NULL) <= 0)
    {
        //WideCharToMultiByteִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pLpsz;
        pLpsz  = NULL;
        return FALSE;
    }
    //���ת����ĳ��ȴ�������������ĳ��ȷ���false
    if (iLen > dwOutSize)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pLpsz;
        pLpsz = NULL;
        return FALSE;
    }
    memset(pOut, 0, dwOutSize);
    memcpy(pOut, pLpsz, (DWORD)iLen);

    if (pOutLen != NULL)
    {
        *pOutLen = (DWORD)iLen;
    }

    delete[] pBuffw;
    pBuffw = NULL;
    delete[] pLpsz;
    pLpsz  = NULL;

    return TRUE;
}


/*********************************************************
������:	UTF82A
��������:	UTF8ת���ֽ�
�������:	cont - UTF8�����ַ���                       
�������:	
����ֵ:	    ���ֽ��ַ���
**********************************************************/
BOOL UTF82A(const char* pCont, char* pOut, DWORD dwOutSize, DWORD* pOutLen)
{
    if (pCont == NULL)
    {
        if (pOutLen != NULL)
        {
            *pOutLen = (DWORD)0;
        }
        return TRUE;
    }

    if (pOut == NULL)
    {
        return FALSE;
    }

    int iNum = MultiByteToWideChar(CP_UTF8, NULL, pCont, -1, NULL, NULL);
    /*lint -e{119} û�ж������*/
    wchar_t* pBuffw = new(std::nothrow) wchar_t[(DWORD)iNum];
    if (pBuffw == NULL)
    {
        return FALSE;
    }

    if (MultiByteToWideChar(CP_UTF8, NULL, pCont, -1, pBuffw, iNum) <= 0)
    {
        //MultiByteToWideCharִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        return FALSE;
    }

    int iLen = WideCharToMultiByte(CP_ACP, 0, pBuffw, iNum - 1, NULL, NULL, NULL, NULL);

    if (iLen <= 0)
    {
        //WideCharToMultiByteִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        return FALSE;
    }

    if ((DWORD)(iLen) > dwOutSize)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        return FALSE;
    }

    /*lint -e{119} û�ж������*/
    char* pLpsz = new char[(DWORD)iLen];
    if (pLpsz == NULL)
    {
        delete[] pBuffw;
        pBuffw = NULL;
        return FALSE;
    }

    if (WideCharToMultiByte(CP_ACP, 0, pBuffw, iNum - 1, pLpsz, iLen, NULL, NULL) <= 0)
    {
        //WideCharToMultiByteִ��ʧ��
        delete[] pBuffw;
        pBuffw = NULL;
        delete[] pLpsz;
        pLpsz = NULL;
        return FALSE;
    }

    memset(pOut, 0, dwOutSize);
    memcpy(pOut, pLpsz, (DWORD)iLen);
    if (pOutLen != NULL)
    {
        *pOutLen = (DWORD)iLen;
    }

    delete[] pBuffw;
    pBuffw = NULL;
    delete[] pLpsz;
    pLpsz = NULL;

    return TRUE;
}

