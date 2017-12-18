/*F***************************************************************************
 * openSMILE - the open-Source Multimedia Interpretation by Large-scale
 * feature Extraction toolkit
 * 
 * (c) 2008-2011, Florian Eyben, Martin Woellmer, Bjoern Schuller: TUM-MMK
 * 
 * (c) 2012-2013, Florian Eyben, Felix Weninger, Bjoern Schuller: TUM-MMK
 * 
 * (c) 2013-2014 audEERING UG, haftungsbeschränkt. All rights reserved.
 * 
 * Any form of commercial use and redistribution is prohibited, unless another
 * agreement between you and audEERING exists. See the file LICENSE.txt in the
 * top level source directory for details on your usage rights, copying, and
 * licensing conditions.
 * 
 * See the file CREDITS in the top level directory for information on authors
 * and contributors. 
 ***************************************************************************E*/


/*  openSMILE component:

example vectorProcessor descendant

*/


#ifndef __CPITCHACF_HPP
#define __CPITCHACF_HPP

#include <core/smileCommon.hpp>
#include <core/vectorProcessor.hpp>

#define COMPONENT_DESCRIPTION_CPITCHACF "This component computes the fundamental frequency and the probability of voicing via an acf and cepstrum based method. The input must be an acf field and a cepstrum field (both generated by a cAcf component)."
#define COMPONENT_NAME_CPITCHACF "cPitchACF"

#undef class
class DLLEXPORT cPitchACF : public cVectorProcessor {
  private:
    int HNR;
    int HNRdB, linHNR;
	  int F0, F0raw;
	  int F0env;
    int voiceProb, voiceQual;
	  int onsFlag;
    double maxPitch;
	  double voicingCutoff;
	  FLOAT_DMEM lastPitch, lastlastPitch, glMeanPitch, pitchEnv;
	  float fsSec;

  protected:
    SMILECOMPONENT_STATIC_DECL_PR

    virtual void fetchConfig();
    //virtual int myConfigureInstance();
    //virtual int myFinaliseInstance();
    //virtual int myTick(long long t);

    //virtual int configureWriter(const sDmLevelConfig *c);

    //virtual void configureField(int idxi, long __N, long nOut);
    //virtual int setupNamesForField(int i, const char*name, long nEl);
	  virtual int setupNewNames(long nEl);
    //virtual int processVectorInt(const INT_DMEM *src, INT_DMEM *dst, long Nsrc, long Ndst, int idxi);
    virtual int processVectorFloat(const FLOAT_DMEM *src, FLOAT_DMEM *dst, long Nsrc, long Ndst, int idxi);
    double computeHNR(const FLOAT_DMEM *a, int f0Idx);
    double computeHNR_dB(const FLOAT_DMEM *a, int f0Idx);
    double computeHNR_lin(const FLOAT_DMEM *a, int f0Idx);

  public:
    SMILECOMPONENT_STATIC_DECL
    
    cPitchACF(const char *_name);
    
    double voicingProb(const FLOAT_DMEM *a, int n, int skip, double *Zcr);
    long pitchPeak(const FLOAT_DMEM *a, long n, long skip);

    virtual ~cPitchACF();
};




#endif // __CPITCHACF_HPP
