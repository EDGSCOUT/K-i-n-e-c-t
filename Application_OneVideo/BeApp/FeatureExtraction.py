__author__ = 'zhangzhan'
# -*- coding: utf-8 -*-
#每个点提取114个特征
#2016.4.7
import sys
import numpy as np
import codecs
import csv
import os
from numpy import zeros
from numpy import genfromtxt
from scipy import stats

currentDir = os.getcwd()
# 从原始数据中读取数据
def dataExtractor(rootName,dirName):
    
    print "Data Reading............"
    #点的序号
    pointSeq = [8,631,356,634,644,659,366,660,355,365,9,376,878,988,375,680,1027,519,879,884,889,214,1058,681,881,886,520,890,989,647,225,880,885,888,4,569,1026,10,245,246,190,887,360,462,678,295,445,903,232,247,75,521,250,891,377,444,900,30,335,336,239,231,29,249,454,367,254,502,503,893,255,213,281,282,224,1186,905,80,248,518,679,570,256,501,892,370,1187,272,906,359,1188,500,645,357,5,633,337,583,1189,369]

    result = []
    for i in ["03"]:
        with codecs.open("%s/%s/%s.csv"%(rootName,dirName,i),"r") as fp:
            for line in fp.readlines():
                line = line.strip("\r\t\n").split(",")
                res = []
                headX = line[4041]
                headY = line[4042]
                headZ = line[4043]

                for p in pointSeq[0:50]:
                    valueX = float(line[3*p])- float(headX)
                    valueY = float(line[3*p+1])-float(headY)
                    valueZ = float(line[3*p+2])-float(headZ)

                    res.append(valueX)
                    res.append(valueY)
                    res.append(valueZ)
                result.append(res)
    data = np.array(result)
    print "Row = %d, Column = %d"%(data.shape[0],data.shape[1])
    return data



#滑动平均滤波，窗口大小设置为3
def removeNoise(data):

    print "Denoising............"
    #矩阵的行数与列数
    rowNum = data.shape[0]
    columnNum = data.shape[1]

    for col in range(columnNum):
        for row in range(rowNum-2):
            data[row,col] =(data[row,col] + data[row+1,col] + data[row+2,col])/3.0
    return data

def dataSlice(data):

    print "Data Slice........."
    # 行数
    rowNum = data.shape[0]
    if rowNum >= 64:

        sliceNum = rowNum/64;
        print "Data Length = %d" % sliceNum
        data = featureExtraction(data,sliceNum)
        return data
    else:

        print "Length of data is less than 64"





def featureExtraction(data,winNum):

    #特征矩阵
    eigenMatrix = zeros((winNum,2400))

    for row in range(winNum):
        if row == winNum:
            beginColNum = 64*row

            endColNum = beginColNum + 64

        else :

            beginColNum = 64*row
            endColNum = beginColNum + 128

        subData = data[beginColNum:endColNum,:]
        #print subData
        rowNum = endColNum - beginColNum
        for colIndex in range(50):
            startFeature = colIndex*48
            startColNum = colIndex*3

            # X,Y,Z轴的标准差
            eigenMatrix[row,startFeature:startFeature+3] = np.std(subData[:,startColNum:startColNum+3],axis=0)
            #X与Y轴的相关系数
            corr1 = np.corrcoef(subData[:,startColNum],subData[:,startColNum+1])
            eigenMatrix[row,startFeature+3] = corr1[0][1]

            #X与Z轴的相关系数
            corr2 = np.corrcoef(subData[:,startColNum],subData[:,startColNum+2])
            eigenMatrix[row,startFeature+4] = corr2[0][1]

            #Y与Z轴的相关系数
            corr3 = np.corrcoef(subData[:,startColNum+1],subData[:,startColNum+2])
            eigenMatrix[row,startFeature+5] = corr3[0][1]

            #计算峰度与偏度
            #skewKurtValue = stats.describe(subData[:,startColNum:startColNum+3])
            kurtValue = stats.kurtosis(subData[:,startColNum:startColNum+3])
            eigenMatrix[row,startFeature+6:startFeature+9] = kurtValue[0:3]+3
            skewValue = stats.skew(subData[:,startColNum:startColNum+3])
            eigenMatrix[row,startFeature+9:startFeature+12] = skewValue[0:3]

            #计算FFT变换

            fftValue = np.abs(np.fft.fft(subData[:,startColNum:startColNum+3],axis=0))

            eigenMatrix[row,startFeature+12] = fftValue[0,0]/rowNum
            eigenMatrix[row,startFeature+13:startFeature+22] = fftValue[1:10,0]
            eigenMatrix[row,startFeature+22] = fftValue[0,1]/rowNum
            eigenMatrix[row,startFeature+23:startFeature+32] = fftValue[1:10,1]
            eigenMatrix[row,startFeature+32] = fftValue[0,2]/rowNum
            eigenMatrix[row,startFeature+33:startFeature+42] = fftValue[1:10,2]

            #功率谱能量的均值和标准差

            eigenMatrix[row,startFeature+42] = np.mean(np.abs(fftValue[:,0])**2)
            eigenMatrix[row,startFeature+43] = np.std(np.abs(fftValue[:,0])**2)
            eigenMatrix[row,startFeature+44] = np.mean(np.abs(fftValue[:,1])**2)
            eigenMatrix[row,startFeature+45] = np.std(np.abs(fftValue[:,1])**2)
            eigenMatrix[row,startFeature+46] = np.mean(np.abs(fftValue[:,2])**2)
            eigenMatrix[row,startFeature+47] = np.std(np.abs(fftValue[:,2])**2)


    print "Row:%d, Column:%d" %(eigenMatrix.shape[0],eigenMatrix.shape[1])
    return eigenMatrix

def PCA(data):

    #基本思路：
    #1 计算样本的平均值
    #2 计算协方差矩阵
    #3 计算协方差矩阵的特征值和特征向量
    #4 将特征值从大到小排序
    #5 保留最大的特征值所对应的N个特征向量
    #6 将数据转换到上述N个特征向量构建的新空间中

    print data.shape

    # 计算样本的平均值
    meanVal = np.mean(data,axis=0)

    meanRemoved = data-meanVal
    # 计算协方差矩阵
    #print meanRemoved
    #covMat = np.cov(meanRemoved,rowvar=0)
    #print "协方差矩阵大小，行数为：%d,列数为：%d"%(covMat.shape[0],covMat.shape[1])
    # 计算协方差矩阵的特征值和特征向量
    #eigVals,eigVects = np.linalg.eig(np.mat(covMat))
    #print "特征值计算完成"
    #特征值从大到小
    #eigValInd = np.argsort(-eigVals)
    #按照贡献率0.95选取特征向量
    #sumTotal = sum(eigVals)
    #print "特征累加和为：%s"%sumTotal
    #sumVal = 0
    #eigIndex = []
    #for i in eigValInd:
    #     sumVal += eigVals[i]
    #     eigIndex.append(i)
    #     if sumVal / sumTotal >= 0.95:
    #         break
    #选取的特征向量
    #redEigVects = eigVects[:,eigIndex]
    #np.savetxt("eigVecMatrix.csv",redEigVects.real,delimiter=",")
    #将为后的数据
    redEigVects= genfromtxt("eigVecMatrix.csv",delimiter=",")
    print "EigenVector:Row:%d,Column:%d"%(redEigVects.shape[0],redEigVects.shape[1])
    #np.savetxt("eigVecMatrix.csv", redEigVects.real, delimiter=",")
    lowDataMat = meanRemoved.dot(redEigVects)
    print "Dimension Reduction,Row:%d,Column:%d"%(lowDataMat.shape[0],lowDataMat.shape[1])
    #np.savetxt("resultMatrix.csv", lowDataMat.real, delimiter=",")
    #print type(meanRemoved)
    return lowDataMat


def predict(data,usrNum):
    #rowNum_1 = data.shape[0]
    rowNum_1 = 1
    esteemFaceScore = 0
    socialFaceScore = 0
    depreFaceScore = 0
    anxietyFaceScore = 0
    sleepFaceScore = 0

    for rn in range(rowNum_1):
        # 自尊 ，线性回归
        #esteemScore +=  -0.0767 * data[rn,0] + -0.0116 * data[rn,2] + 0.0208 * data[rn,3] + -0.05 * data[rn,6] + 0.0398 * data[rn,7] +-0.034  * data[rn,8] +0.0483 * data[rn,10] + -0.036  * data[rn,11] +0.0371 * data[rn,13] + -0.0497 * data[rn,14] + 0.0899 * data[rn,15] + 0.079 * data[rn,16] + -0.0366 * data[rn,17] + -0.2005 * data[rn,18] + 23.7795
        esteemFaceScore += -0.0767 * data[rn,1] + -0.0345 * data[rn,2] + -0.1387 * data[rn,3] + 0.0913 * data[rn,4] + -0.0686 * data[rn,5] + -0.0878 * data[rn,6] + 0.05 * data[rn,7] + -0.0627 * data[rn,9] + -0.3041 * data[rn,10] + 0.0686 * data[rn,11] + 0.0746 * data[rn,12] + 0.2698 * data[rn,13] + -0.1687 * data[rn,14] + 0.1553 * data[rn,15] + 31.9104
        # 社会支持，线性回归
        #socialScore += 0.0541 * data[rn,1] + -0.2432 * data[rn,6] + -0.19 * data[rn,8] + -0.3275 * data[rn,9] + 0.1632 * data[rn,11] + 0.4261 * data[rn,12] + 0.4282 * data[rn,13] + 0.4414 * data[rn,15] + -0.4074 * data[rn,16] + -0.5975 * data[rn,17] + -0.6888 * data[rn,18] + 35.9392
        socialFaceScore += -0.1571 * data[rn,1] + 0.1311 * data[rn,2] + -0.054  * data[rn,3] + 0.0837 * data[rn,4] + -0.0651 * data[rn,5] + -0.1132 * data[rn,6] + -0.058 * data[rn,7] + 0.0816 * data[rn,8] + -0.1093 * data[rn,10] + -0.0916 * data[rn,11] + -0.4148 * data[rn,12] + -0.2298 * data[rn,13] + 33.4441
        # 抑郁，线性回归
        #depreScore += -0.0224 * data[rn,0] +-0.0167 * data[rn,2] + 0.0771 * data[rn,6] + -0.0328 * data[rn,7] + 0.0477 * data[rn,8] + -0.0832 * data[rn,13] + -0.042  * data[rn,14] + -0.1197 * data[rn,15] + 0.1217 * data[rn,17] + 0.1081 * data[rn,18] + 4.2433
        depreFaceScore += -0.0167 * data[rn,0] + 0.0451 * data[rn,1] + -0.0219 * data[rn,2] + 0.0389 * data[rn,3] + -0.0415 * data[rn,4] + 0.0275 * data[rn,5] + -0.0831 * data[rn,8] + 0.0611 * data[rn,9] + 0.0772 * data[rn,10] + -0.0782 * data[rn,11] + 0.1088 * data[rn,12] + 0.1136 * data[rn,13] + 4.1546

        # 焦虑，线性回归
        #anxietyScore += 0.0151 * data[rn,0] + -0.0169 * data[rn,1] +0.018  * data[rn,2] +0.0654 * data[rn,6] +0.0898 * data[rn,8] + 0.0336 * data[rn,9] +0.0386 * data[rn,10] +-0.0544 * data[rn,11] +-0.0923 * data[rn,12] +-0.105 * data[rn,13] + -0.084 * data[rn,15] + 0.0595 * data[rn,16] + 0.1107 * data[rn,17] + 0.1881 * data[rn,18] + 2.3992
        anxietyFaceScore += -0.0505 * data[rn,0] +0.062 * data[rn,1] + -0.0222 * data[rn,2] + 0.0798 * data[rn,3] + -0.1156 * data[rn,4] + -0.0654 * data[rn,8] +  0.077 * data[rn,9] + 0.051 * data[rn,10] + -0.2172 * data[rn,11] + 0.0494 * data[rn,12] + 0.3341 * data[rn,13] + 0.1979 * data[rn,14] + 3.9564

        # 睡眠，线性回归
        #sleepScore += 0.024 * data[rn,2] +0.154 * data[rn,6] + -0.0649 * data[rn,7] + 0.1456 * data[rn,8] + -0.0753 * data[rn,12] + -0.1146 * data[rn,13] + -0.1061 * data[rn,14] + -0.1042 * data[rn,15] + -0.1041 * data[rn,16] + 0.2056 * data[rn,17] + 0.5181 * data[rn,18] + 4.308
        sleepFaceScore += -0.0187 * data[rn,0] + 0.0113 * data[rn,1] + 0.023 * data[rn,4] + 0.0252 * data[rn,5] + -0.0357 * data[rn,6] + 0.0246 * data[rn,7] + -0.0309 * data[rn,8] + 0.074 * data[rn,9] + 0.1124 * data[rn,10] + -0.2382 * data[rn,11] + -0.1693 * data[rn,12] + 0.2877 * data[rn,13] + -0.0365 * data[rn,14] + 0.0605 * data[rn,15] +3.956

    
	# 面部数据，自尊，社会支持，抑郁，焦虑和睡眠求均值
    esteemFaceScore /= rowNum_1

    socialFaceScore /= rowNum_1

    depreFaceScore /= rowNum_1

    anxietyFaceScore /= rowNum_1

    sleepFaceScore /= rowNum_1

   
	# 自尊，社会支持，抑郁，焦虑，和睡眠
    esteemScore = esteemFaceScore

    socialScore = socialFaceScore

    depreScore = depreFaceScore

    anxietyScore = anxietyFaceScore

    sleepScore = sleepFaceScore



    fp = open("UserReport/%s-result.txt"%usrNum,"w")
	
    fp.write("              2017年“全国科普日”北京主场活动.国防科技专题展\n")
	
    fp.write("                      基于行为分析的心理特征自动识别\n")

    fp.write("--------------------------------------------------------------------------------\n")

    fp.write("感谢您的参与！我们根据面部变化预测您的自尊、社会支持、抑郁、焦虑和睡眠得分。\n\n")

    fp.write("用户编号： %s\n"%usrNum)
    fp.write("\n")

    fp.write("自尊得分：%d\n"%esteemScore)
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("自尊是指一个人基于自我评价产生和形成的情感体验。自尊有强弱之分，过强则成虚荣心，过弱则变成自卑。\n")
    
    if esteemScore>=10 and esteemScore <= 15:
        fp.write("表明：你对自己缺乏信心，尤其是在陌生人和上级面前，你总是感到自己事事都不如别人，你时常感到自卑。\n")
        fp.write("建议：需要大大提高自信心。\n")
    elif esteemScore > 15 and esteemScore <= 25:

        fp.write("表明：你对自己感觉既不是太好，也不是太不好。你在某些场合下对自我感到相当自信，但在其它场合却感到相当自卑。\n")
        fp.write("建议：你需要稳定你的自信心。\n")
    elif esteemScore > 25 and esteemScore <= 35:
        fp.write("表明：你对自己感觉十分良好。在大多数场合下，你都对自我充满了自信，你不会因为在陌生人或上级面前感到紧张，也不会因为没有经验就不敢尝试。\n")
        fp.write("建议：你需要在不同场合下调试你的自信心\n")
    elif esteemScore>35 and esteemScore<=40:
        fp.write("表明：你对自己感觉太好了。在几乎所有场合下，你都对自我充满了自信，你甚至不知道什么叫自卑。\n")
        fp.write("建议：需要学会控制自信心，变得自谦一些。\n")
		
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("\n")

    fp.write("社会支持得分：%d\n"%socialScore)
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("在心理学中，所谓的社会支持指的是一个人从自己的社会关系（家人、朋友、同事等）中获得的客观支持以及个人对这种支持的主观感受。社会支持不仅指物质上的条件和资源也包括在情感上的支持。\n")
    
	
    if socialScore < 20:
        fp.write("表明：你获得的来自他人的支持较少，遇到困难或问题时很少有人能够帮助或支持你\n")
        fp.write("建议：发展新的人际关系，比如结交新朋友，多跟同事、亲人联络\n")
    elif socialScore >=20 and socialScore <=30:
        fp.write("表明：你能够获得一定的来自他人的支持，遇到困难或问题时有人能够提供帮助或支持\n")
        fp.write("建议：维持现有的人际关系并且可以有意识地建立新的人际关系\n")
    elif socialScore > 30 and socialScore <=40:
        fp.write("表明：你获得的来自他人的支持是比较多的，遇到困难或问题时有很多人可以帮助或支持你\n")
        fp.write("建议：继续保持现有人际关系\n")
        #fp.write("正常情况：总分≥20分 （分数越高，社会支持度越高）\n小于20，为获得社会支持较少；\n20-30，为具有一般社会支持度；\n30-40，为具有满意的社会支持度。\n")
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("\n")
    fp.write("抑郁得分：%d\n"%depreScore)
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("抑郁是一种情绪低沉并伴随兴趣减退、精力不足、体重或食欲剧烈变化等现象的一种心理状态。长期出现抑郁症状并因此遭受明显的痛苦或损失是一种心理不健康的状态，即患有抑郁症。\n")
    
	
    if depreScore>=0 and depreScore <= 4:
        fp.write("表明：你属于没有抑郁的人群，心理健康状况良好。\n")
        fp.write("建议：注意自我保重\n")
    elif depreScore > 4 and depreScore <= 9:
        fp.write("表明：你可能有轻微抑郁症，心理健康状况有所失衡。\n")
        fp.write("建议：建议咨询心理医生或心理医学工作者\n")
    elif depreScore > 9 and depreScore <= 14:
        fp.write("表明：可能有中度抑郁症\n")
        fp.write("建议：最好咨询心理医生或心理医学工作者，及时的干预能够避免抑郁症状的进一步发展\n")
    elif depreScore > 14 and depreScore <= 19:
        fp.write("表明：可能有中重度抑郁症\n")
        fp.write("建议：建议咨询心理医生或精神科医生，专业人员可以帮助你减轻目前的症状。\n")
    elif depreScore > 19 and depreScore <= 27:
        fp.write("表明：可能有重度抑郁症\n")
        fp.write("建议：一定要看心理医生或精神科医生，及时就医能够帮助你缓解目前的痛苦。\n")
    #fp.write("0-4  没有抑郁症     （注意自我保重\n5-9  可能有轻微抑郁症  (建议咨询心理医生或心理医学工作者)\n10-14 可能有中度抑郁症  (最好咨询心理医生或心理医学工作者)\n15-19 可能有中重度抑郁症 (建议咨询心理医生或精神科医生)\n20-27 可能有重度抑郁症  (一定要看心理医生或精神科医生)\n")
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("\n")



    fp.write("焦虑得分：%d\n"%anxietyScore)
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("焦虑是一种难以控制的忧虑情绪，是大部分时间内过度或持续担忧的心理状态。长期出现焦虑症状并因此遭受明显的痛苦或损失是一种心理不健康的状态，即患有焦虑症。\n")
    
    if anxietyScore >= 0 and anxietyScore <= 4:
        fp.write("表明：你属于没有焦虑症的人群，心理状态良好。\n")
        fp.write("建议：注意自我保重\n")
    elif anxietyScore > 4 and anxietyScore <= 9:
        fp.write("表明：可能有轻微焦虑症\n")
        fp.write("建议：建议咨询心理医生或心理医学工作者\n")
    elif anxietyScore > 9 and anxietyScore <= 13:
        fp.write("表明：可能有中度焦虑症\n")
        fp.write("建议：最好咨询心理医生或心理医学工作者\n")
    elif anxietyScore > 13 and anxietyScore <= 18:
        fp.write("表明：可能有中重度焦虑症\n")
        fp.write("建议：建议咨询心理医生或精神科医生\n")

    elif anxietyScore > 18 and anxietyScore <= 21:
        fp.write("表明：可能有重度焦虑症\n")
        fp.write("建议：一定要看心理医生或精神科医生\n")
    #fp.write("0-4  没有焦虑症      (注意自我保重)\n5-9  可能有轻微焦虑症  (建议咨询心理医生或心理医学工作者)\n10-13  可能有中度焦虑症  (最好咨询心理医生或心理医学工作者)\n14-18 可能有中重度焦虑症 (建议咨询心理医生或精神科医生)\n19-21  可能有重度焦虑症  (一定要看心理医生或精神科医生)\n")
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("\n")



    fp.write("睡眠得分：%d\n"%sleepScore)
    fp.write("--------------------------------------------------------------------------------\n")
    fp.write("睡眠具有重要的生理调节功能，对人的记忆、情绪等有重要的影响。睡眠质量不好会影响人们的工作、学习，好的睡眠质量则有助于顺利的工作和学习。\n")
    
    if sleepScore >= 0 and sleepScore <= 5:
        fp.write("表明：睡眠质量很好\n")
        fp.write("建议：作息习惯好，继续保持\n")
    elif sleepScore > 5 and sleepScore <= 10:
        fp.write("表明：睡眠质量还行\n")
        fp.write("建议：作息习惯不错，请有意识地保持\n")
    elif sleepScore > 10 and sleepScore <= 15:
        fp.write("表明：睡眠质量一般\n")
        fp.write("建议：调整作息、改善自己的睡眠，例如不熬夜、睡前不玩手机\n")

    elif sleepScore > 15 and sleepScore <= 21:
        fp.write("表明：睡眠质量很差\n")
        fp.write("建议：急需改善睡眠，建议寻求医生的建议，在医生的帮助下合理地改善睡眠状况。\n")
    fp.write("--------------------------------------------------------------------------------\n\n")

    fp.write("--------------------------------------------------------------------------------")
	
    fp.write("                                中国科学院心理研究所")
	
    #fp.write("0-5分   睡眠质量很好 （作息习惯好，继续保持）\n6-10分   睡眠质量还行 （作息习惯较好，继续保持）\n11-15分   睡眠质量一般 （需要调节作息规律）\n16-21分   睡眠质量很差  （需要寻求医生的建议）\n")
    fp.close()
def audioFeatureExtraction(rootName,dirName):

    curDir = os.getcwd()

    audioResult = []
    for wavName in ["16.wav","17.wav","18.wav"]:
        os.system("%s/openSMILE-2.1.0/bin/Win32/SMILExtract_Release.exe -C extract_functional_features.conf -I %s/%s/%s -S result.lsvm"%(curDir,rootName,dirName,wavName))
        audioRes = []
        fp = open("result.lsvm","r")
        line = fp.readline()
        fp.close()
        line = line.strip("\r\t\n").split(" ")
        for value in line[1:-1]:
            value_1 = value.split(":")[1]
            audioRes.append(float(value_1))
        audioResult.append(audioRes)

    return np.array(audioResult)


if __name__ == "__main__":
    rootName = "e:/VoiceData"
    dirList = os.listdir(rootName)
    usrList = []
    fp = open("usrList.csv","r")
    lines = fp.readlines()
    for line in lines:
        line = line.strip("\r\t\n")
        usrList.append(line)

    for dirName in dirList:
        try:

            if dirName not in usrList:
                print "USER ID: %s"%dirName
                fp = open("usrList.csv","a")
                fp.write(dirName+"\n")
                fp.close()
                #读取面部数据
                faceData = dataExtractor(rootName,dirName)
                #去噪
                faceData = removeNoise(faceData)
                # 切片
                faceData = dataSlice(faceData)
                # 数据降维
                faceData = PCA(faceData)

                #print faceData.shape

                #读取语音数据
                #voiceData = audioFeatureExtraction(rootName,dirName)
                #print voiceData
                #print voiceData.shape

                # 模型预测
                predict(faceData,dirName)
            else:

                print "USER ID EXIST"
        except:
            continue

# 语音特征提取


#resultMatrix = np.append(resultMatrix,data,axis=0)
#print resultMatrix.shape









