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

This component computes pitch via the Harmonic Product Spectrum method.
As input it requires FFT magnitude data. 
Note that the type of input data is not checked, thus be careful when writing your configuration files!

*/


#ifndef __CPITCHBASE_HPP
#define __CPITCHBASE_HPP

#include <core/smileCommon.hpp>
#include <core/vectorProcessor.hpp>

#define COMPONENT_DESCRIPTION_CPITCHBASE "Base class for all pitch classes, no functionality on its own!"
#define COMPONENT_NAME_CPITCHBASE "cPitchBase"


#undef class
class DLLEXPORT cPitchBase : public cVectorProcessor {
  private:
    const char *inputFieldPartial;
    int nCandidates;
    int scores, voicing;
    int F0C1, voicingC1;
    int F0raw;
    int voicingClip;

    double maxPitch, minPitch;

    FLOAT_DMEM *inData;
    FLOAT_DMEM *f0cand, *candVoice, *candScore;

  protected:
    SMILECOMPONENT_STATIC_DECL_PR
    
    FLOAT_DMEM voicingCutoff;
    int octaveCorrection;

    virtual void fetchConfig();
	  virtual int setupNewNames(long nEl);
    
    //virtual int processVectorInt(const INT_DMEM *src, INT_DMEM *dst, long Nsrc, long Ndst, int idxi);
    virtual int processVectorFloat(const FLOAT_DMEM *src, FLOAT_DMEM *dst, long Nsrc, long Ndst, int idxi);

    // to be overwritten by child class:
    virtual int pitchDetect(FLOAT_DMEM * inData, long N_, double _fsSec, double baseT, FLOAT_DMEM *f0cand, FLOAT_DMEM *candVoice, FLOAT_DMEM *candScore, long nCandidates);
    virtual int addCustomOutputs(FLOAT_DMEM *dstCur, long NdstLeft);

  public:
    SMILECOMPONENT_STATIC_DECL
    
    cPitchBase(const char *_name);
    
    virtual ~cPitchBase();
};




#endif // __CPITCHBASE_HPP
