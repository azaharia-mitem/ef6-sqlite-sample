Entity Framework 6 with SQLite
==============================

## Package

아래 두 Nuget Package 를 추가합니다.

프로젝트의 packages.config 파일의 내용을 확인하십시오.

### NuGet Package
* System.Data.SQLite
* System.Data.SQLite.EF6

### Packages
* EntityFramework
* System.Data.SQLite
* System.Data.SQLite.Core
* System.Data.SQLite.EF6
* System.Data.SQLite.Linq

## config

app.config 혹은 web.config 에는 아래와 같은 요소 Element 가 필요합니다.

NuGet Package 추가시 일부 config가 입력됩니다만, 다시 한번 확인해야 합니다.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <configSections>
        <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
        <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
    </configSections>
    <startup>
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
    </startup>
    <connectionStrings>
        <add name="DatabaseContext" connectionString="Data Source=|DataDirectory|data.db" providerName="System.Data.SQLite.EF6" />
    </connectionStrings>
    <entityFramework>
        <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
            <parameters>
                <parameter value="v12.0" />
            </parameters>
        </defaultConnectionFactory>
        <providers>
            <provider invariantName="System.Data.SQLite" type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6, Version=1.0.99.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139"/>
            <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
            <provider invariantName="System.Data.SQLite.EF6" type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6" />
        </providers>
    </entityFramework>
    <system.data>
        <DbProviderFactories>
            <remove invariant="System.Data.SQLite.EF6" />
            <add name="SQLite Data Provider (Entity Framework 6)" invariant="System.Data.SQLite.EF6" description=".NET Framework Data Provider for SQLite (Entity Framework 6)" type="System.Data.SQLite.EF6.SQLiteProviderFactory, System.Data.SQLite.EF6" />
            <remove invariant="System.Data.SQLite" />
            <add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".NET Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite" />
        </DbProviderFactories>
    </system.data>
</configuration>
```

## 파일 추가

SQLite.Interop.dll 파일을 추가합니다. System.Data.SQLite.Core 패키지 내용 중 포함되어져 있으므로 프로젝트에 링크로 추가하면 됩니다.
프로젝트에 파일 추가 후 파일의 속성 창을 열고 아래와 같이 변경합니다.

* 빌드작업: 내용
* 출력 디렉터리에 복사: 새 버전이면 복사

## 프로젝트 파일 구조
```plaintext
│  App.config
│  data.db
│  Form1.cs
│  Form1.Designer.cs
│  Form1.resx
│  packages.config
│  Program.cs
│  SqliteSmaple.csproj
│
├─Lib
│      DatabaseContext.cs
│
├─Model
│      Todo.cs
│
├─Properties
│      AssemblyInfo.cs
│      Resources.Designer.cs
│      Resources.resx
│      Settings.Designer.cs
│      Settings.settings
│
├─x64
│      SQLite.Interop.dll
└─x86
       SQLite.Interop.dll
```

## EF6 SQLite 참조
Code First EF를 구현하려고 했으나, EF6 은 SQLite 에 대한 Migration 을 공식적으로 지원하지 않습니다.

그래서, `data.db` 파일을 만들고, 테이블을 생성한 후 작업을 해야 합니다.

> SQL Server Compact/SQLite Toolbox 확장을 설치하면 편리하게 파일 생성 및 테이블 생성을 할 수 있습니다.

EF7 부터는 지원할 예정이라고 하니, EF7 정식버전이 출시되면 다시 한번 시도해봐야겠습니다.

## 프로젝트 설명

간단한 Todo 목록을 입력하는 응용 프로그램입니다.

Model 디렉터리 하위의 Todo 클래스가 Todo 테이블과 맵핑되는 클래스입니다.

Lib 디렉터리 하위의 DatabaseContext가 데이터베이스와 연결되어 엔티티를 관리하는 클래스입니다.



[stackoverflow: Entity Framework 6 with SQLite 3 Code First - Won't create tables](http://stackoverflow.com/questions/22174212/entity-framework-6-with-sqlite-3-code-first-wont-create-tables)
