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

example of a vector processor

*/



#include <examples/exampleVectorProcessor.hpp>

#define MODULE "cExampleVectorProcessor"


SMILECOMPONENT_STATICS(cExampleVectorProcessor)

SMILECOMPONENT_REGCOMP(cExampleVectorProcessor)
{
  SMILECOMPONENT_REGCOMP_INIT
  scname = COMPONENT_NAME_CEXAMPLEVECTORPROCESSOR;
  sdescription = COMPONENT_DESCRIPTION_CEXAMPLEVECTORPROCESSOR;

  // we inherit cVectorProcessor configType and extend it:
  SMILECOMPONENT_INHERIT_CONFIGTYPE("cVectorProcessor")

  // if the inherited config type was found, we register our configuration variables
  SMILECOMPONENT_IFNOTREGAGAIN( {} // <- this is only to avoid compiler warnings...
    // name append has a special role: it is defined in cDataProcessor, and can be overwritten here:
	// if you set description to NULL, the existing description will be used, thus the following call can
	// be used to update the default value:
    //ct->setField("nameAppend",NULL,"processed");

    // this is an example for adding an integer option:
	//ct->setField("inverse","1 = perform inverse FFT",0);
  )

  // The configType gets automatically registered with the config manger by the SMILECOMPONENT_IFNOTREGAGAIN macro
  
  // we now create out sComponentInfo, including name, description, success status, etc. and return that
  SMILECOMPONENT_MAKEINFO(cExampleVectorProcessor);
}

SMILECOMPONENT_CREATE(cExampleVectorProcessor)

//-----

cExampleVectorProcessor::cExampleVectorProcessor(const char *_name) :
  cVectorProcessor(_name)
{

}

void cExampleVectorProcessor::fetchConfig()
{
  cVectorProcessor::fetchConfig();
  
/*
  inverse = getInt("inverse");
  if (inverse) {
    SMILE_DBG(2,"transformFFT set for inverse FFT",inverse);
    inverse = 1;  // sign of exponent
  } else {
    inverse = -1; // sign of exponent
  }
  */
}

/*
int cExampleVectorProcessor::myConfigureInstance()
{
  int ret=1;
  ret *= cVectorProcessor::myConfigureInstance();
  if (ret == 0) return 0;

//...


  return ret;
}
*/

/*
int cExampleVectorProcessor::configureWriter(const sDmLevelConfig *c)
{

  // you must return 1, in order to indicate configure success (0 indicated failure)
  return 1;
}

*/

// optional
/*
int cExampleVectorProcessor::setupNamesForField(int i, const char*name, long nEl)
{
  return cVectorProcessor::setupNamesForField(i,name,nEl);
}
*/

// use this to allocate data like filter coefficient arrays, etc...
void cExampleVectorProcessor::configureField(int idxi, long __N, long nOut)
{
//     const sDmLevelConfig *c = reader->getLevelConfig();
// idxc = getFconf(idxi);
}



/*
int cExampleVectorProcessor::myFinaliseInstance()
{
  int ret=1;
  ret *= cVectorProcessor::myFinaliseInstance();
//.....
  return ret;
}
*/

// a derived class should override this method, in order to implement the actual processing
int cExampleVectorProcessor::processVectorInt(const INT_DMEM *src, INT_DMEM *dst, long Nsrc, long Ndst, int idxi) // idxi=input field index
{
  // do domething to data in *src, save result to *dst
  // NOTE: *src and *dst may be the same...
  
  return 1;
}

// a derived class should override this method, in order to implement the actual processing
int cExampleVectorProcessor::processVectorFloat(const FLOAT_DMEM *src, FLOAT_DMEM *dst, long Nsrc, long Ndst, int idxi) // idxi=input field index
{
  // do domething to data in *src, save result to *dst
  // NOTE: *src and *dst may be the same...

  return 1;
}

cExampleVectorProcessor::~cExampleVectorProcessor()
{
}

