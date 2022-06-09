# 프로젝트 이름
MVVM WPFScheduler

## 프로젝트 설명
개발기간 3개월   

C# WPF를 공부했지만 아직 부족한점이 많아   
더욱 숙련도를 올리기위해 프로젝트를 진행하였다.   

월간,주간,일간 별로 보여주는 달력에   
자신의 스케쥴을 등록할 수 있는 기능을 추가 하였다.   

C# WPF을 사용하여 작성하였다.    

### 개발환경

```
Windows 10
C#
Visual Studio 2019
.NET Framework, Version=v4.7.2
MSSQL
```

### 추가종속성

```
Microsoft.Extensions.Hosting
dapper
```
nuget패키지로 추가하였음

## 실행 영상

https://youtu.be/rRa4gNm6LII   

## 기능

* Event를 자동정렬하여 Month,week,day별로 보여줌
* Event신규작성 추가
* 기존Event 수정,삭제 (우클릭)
* MonthView에서 날짜 클릭시 해당 날짜 이벤트 리스트를 보여줌
* Month,WeekView에서 날짜 더블클릭시 해당날짜 dayView로 이동

* Event를 DATABASE에 CRUD
* LOGIN기능/DATABASE CRUD(Update Delete 미구현)

## DataBase

ORM을 위해 Dapper Library를 사용 하였다.


* EVENT 테이블

![Event_TB](https://user-images.githubusercontent.com/92092084/172597827-29eebf78-1bab-416d-a1d6-f57cb822db94.png)


* LOGIN 테이블


![Login_TB](https://user-images.githubusercontent.com/92092084/172597829-73a028fa-e471-4e4b-8560-2a311eb85f79.png)


* 프로시저


![Procedures](https://user-images.githubusercontent.com/92092084/172597832-e3bd61db-405b-4cfc-900c-6841d83af11b.png)




![storedProcedure](https://user-images.githubusercontent.com/92092084/172597842-87f3516a-e7f5-42f8-8999-4ecfb7814c42.png)


* EventRepository



![Repository](https://user-images.githubusercontent.com/92092084/172597836-c9057421-a9c9-4239-8acf-7758e5f66eaa.png)

## Dependency Injection

Microsoft.Extensions.Hosting 추가

![DependencyInjection](https://user-images.githubusercontent.com/92092084/172597819-6ec6fda4-20bb-4a35-b07d-dd5f89c241bb.png)


## 프로젝트에서 신경쓴점

1. 디렉토리 구조   

  root 디렉토리에 파일을 모아두면 한눈에 파악이 안되고, 일관성이 없다.   
  MVVM패턴에서는 어떻게 디렉토리를 나누는지 알아보고,고민하였으나   
  프로젝트 상황에 따라 다를 수 있는 부분이기에, 명확한 답을 내지 못하였다.   
  Model,ViewModel,View / Converter /Resource 등 기능별로 나누어 프로젝트를 진행하였다.   

2. 엄격한 MVVM에서 지켜야할것   

  MVVM패턴의 의의는 Model과 View를 분리함에 있다.(의존성 제거)   
  Model과 View는 서로를 알수 없어야하며, ViewModel과 View도 Binding을 통해 연결해야한다.   
  View와 Model(ViewModel)간에는 어떠한 명시적 참조가 있어선 안된다.   

  View에 ViewModel을 선언하는 등의 패턴에 위반하는 코드들은 전면 수정하였다.   


3. Dependency Injection   

  MainVM에서 많은 변수들을 관리하고, 많은 일들을 처리한다.   
  종속성 문제를 해결하기위해 Dependency Injection을 사용하였다.   
  책임을 줄이기위하여 각 서비스별로 핵심 기능을 interface로 추출해 추상화 하였다.   


## 마치며

Login ,password 등 보안에 관련된 부분이 부족했다고 생각한다.   
기능을 구현할때 본인의 프로그래밍 실력, 시간, 혹은 프레임워크 자체적 한계로 타협한 부분이 많았다.   

처음 프로젝트를 진행할때에 MVVM에 대한 이해가 부족하여 해맸으나   
프로젝트를 진행하며 WPF,MVVM에 대해 많은것을 배웠다.   



