# Generic "Resulting-Sum" License Key Generator Library

A generator for human-friendly, readable license keys and serial numbers by www.barbez.eu. 

Produces license keys you don't mind spelling out loud to another person.

The Barbez.eu license key generator is ideal for all your license key or serial number generation needs, producing keys or serials that are easy to read out loud one human to another. Yet, the algorithm is quick enough to generate a decent amount of them in a short period of time.

## Table of Contents
0. [NuGet Packages and Demo app](#nuget-packages-and-demo-app)
1. [Features](#features)
2. [Custom Resulting-Sum Algorithm](#custom-resulting-sum-algorithm)
3. [Performance](#performance)
4. [Stats](#stats)
5. [Graphs](#graphs)

## NuGet Packages and Demo app

Get the **generator** NuGet package from https://www.nuget.org/packages/BarbezDotEu.License.Generation/ or use 
> Install-Package BarbezDotEu.License.Generation -Version 1.2.0

Get the **verifier** NuGet package from https://www.nuget.org/packages/BarbezDotEu.License.Verification/ or use
> Install-Package BarbezDotEu.License.Verification -Version 1.2.0

A demo project using this generic library can be found at https://github.com/hannesbarbez/BarbezDotEu.License.Generator.WinForms.

## Features
- Full multithreading, thanks to .NET Core’s TPL (Task Parallel Library);
- Custom “resulting-sum” algorithm to generate a bunch of unique license keys quickly;
- On-the-fly license key validator;
- Decoupling, e.g. strict separation between UI and logic;
- Generated license keys that are easy for people to spell out loud;
- Native support for async/await.

## Custom Resulting-Sum Algorithm

Lacking academic knowledge on serial number or license key generation algos, the generator algorithm is my own idea, so I’ve decided to dub it “resulting-sum”. More info on the exact workings to follow, for now the source code should suffice to explain its inner workings.



## Performance
After running a couple of tests on an AMD Ryzen 1600 (6 cores, 12 threads) based machine, I compiled a short table with the results.

On this hardware, a ResultingSum of 68 takes Y time to render/generate X number of unique keys. The sweet-spots are marked in bold in the below table.

Results will differ for other ResultingSum values. For a value of 68, 30 000 unique keys are generated in under 9 seconds.

The going gets tougher when generating 150 000 unique keys in exactly 4 minutes. Lawd.

## Stats
*Note: these stats are from the initial version of the NuGet package. Newer versions of the package are quicker, yet the overal trend of the graphs remains.*

A "resulting sum" of 68 takes Y time to render/generate X number of unique keys on a Ryzen 1600 based machine.

| Keys (X) | Time (ms, Y) | ms/key (=Y/X) |
| -------- | ------------ | ------------- |
| 1 | **12** | **12.00** |
| 10 | **13** | **1.30** |
| 100 | **32** | **0.32** |
| 1 000 | **146** | **0.15** |
| 2 500 | **358** | **0.14** |
| 5 000 | **719** | **0.14** |
| 10 000 | **1 500** | **0.15** |
| 20 000 | **4 800** | **0.24** |
| **30 000** | **8 600** | **0.29** |
| 50 000 | 21 300 | 0.43 |
| 62 500 | 31 500 | 0.50 |
| 75 000 | 43 800 | 0.58 |
| 87 500 | 66 000 | 0.75 |
| 100 000 | 84 000 | 0.84 |
| 150 000 | 240 000 | 1.60 |

## Graphs
*Note: these stats are from the initial version of the NuGet package. Newer versions of the package are quicker, yet the overal trend of the graphs remains.*

![Y time needed (ms) to generate X number of keys.](https://barbez.eu/wp-content/uploads/2020/09/image-4.png)
![Y time needed (ms) per key when generating X number of keys.](https://barbez.eu/wp-content/uploads/2020/09/image-3.png)

# BarbezDotEu.License.Generation

## Contents

- [KeyGenerator](#T-BarbezDotEu-License-Generation-KeyGenerator 'BarbezDotEu.License.Generation.KeyGenerator')
  - [#ctor(resultingSum,divider)](#M-BarbezDotEu-License-Generation-KeyGenerator-#ctor-System-Int32,System-String- 'BarbezDotEu.License.Generation.KeyGenerator.#ctor(System.Int32,System.String)')
  - [EXCEPTION](#F-BarbezDotEu-License-Generation-KeyGenerator-EXCEPTION 'BarbezDotEu.License.Generation.KeyGenerator.EXCEPTION')
  - [GenerateKeys(numberOfKeys,excludedKeys)](#M-BarbezDotEu-License-Generation-KeyGenerator-GenerateKeys-System-UInt32,System-String[]- 'BarbezDotEu.License.Generation.KeyGenerator.GenerateKeys(System.UInt32,System.String[])')

<a name='T-BarbezDotEu-License-Generation-KeyGenerator'></a>
## KeyGenerator `type`

BarbezDotEu.License.Generation

Basic key generator class.

<a name='M-BarbezDotEu-License-Generation-KeyGenerator-#ctor-System-Int32,System-String-'></a>
### #ctor(resultingSum,divider) `constructor`

Constructs a new [KeyGenerator](#T-BarbezDotEu-License-Generation-KeyGenerator 'BarbezDotEu.License.Generation.KeyGenerator').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resultingSum | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The expected "sum" of the key. All keys share the same "sum", which is what makes the key validatable. Negative or default values are not valid. |
| divider | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The desored division in between segments of the license key. |

<a name='F-BarbezDotEu-License-Generation-KeyGenerator-EXCEPTION'></a>
### EXCEPTION `constants`

One or more parameters are invalid. NULL, negative, empty or default values are not valid parameters.

<a name='M-BarbezDotEu-License-Generation-KeyGenerator-GenerateKeys-System-UInt32,System-String[]-'></a>
### GenerateKeys(numberOfKeys,excludedKeys) `method`

Generates a number of license keys.

##### Returns

The generated license keys.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| numberOfKeys | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | The amount of keys to generate. |
| excludedKeys | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Keys that cannot be present in the resulting key set. |

# BarbezDotEu.License.Verification

## Contents

- [KeyVerificator](#T-BarbezDotEu-License-Verification-KeyVerificator 'BarbezDotEu.License.Verification.KeyVerificator')
  - [#ctor(resultingSum,divider)](#M-BarbezDotEu-License-Verification-KeyVerificator-#ctor-System-Int32,System-String- 'BarbezDotEu.License.Verification.KeyVerificator.#ctor(System.Int32,System.String)')
  - [EXCEPTION](#F-BarbezDotEu-License-Verification-KeyVerificator-EXCEPTION 'BarbezDotEu.License.Verification.KeyVerificator.EXCEPTION')
  - [ValidKey()](#M-BarbezDotEu-License-Verification-KeyVerificator-ValidKey-System-String,System-String,System-String,System-String,System-String- 'BarbezDotEu.License.Verification.KeyVerificator.ValidKey(System.String,System.String,System.String,System.String,System.String)')
  - [ValidateSegment(segment)](#M-BarbezDotEu-License-Verification-KeyVerificator-ValidateSegment-System-String- 'BarbezDotEu.License.Verification.KeyVerificator.ValidateSegment(System.String)')
  - [VerifyKey()](#M-BarbezDotEu-License-Verification-KeyVerificator-VerifyKey-System-String,System-String,System-String,System-String,System-String- 'BarbezDotEu.License.Verification.KeyVerificator.VerifyKey(System.String,System.String,System.String,System.String,System.String)')
  - [VerifyKey(key)](#M-BarbezDotEu-License-Verification-KeyVerificator-VerifyKey-System-String- 'BarbezDotEu.License.Verification.KeyVerificator.VerifyKey(System.String)')

<a name='T-BarbezDotEu-License-Verification-KeyVerificator'></a>
## KeyVerificator `type`

BarbezDotEu.License.Verification

Basic key verifier class.

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-#ctor-System-Int32,System-String-'></a>
### #ctor(resultingSum,divider) `constructor`

Constructs a new basic key verifier (verificator, lat.)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resultingSum | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The resulting sum that has to be met for a key to be valid. |
| divider | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The divider (e.g. "-") expected to be found in between different segments of a key. |

<a name='F-BarbezDotEu-License-Verification-KeyVerificator-EXCEPTION'></a>
### EXCEPTION `constants`

One or more parameters are invalid. NULL, negative, empty or default values are not valid parameters.

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-ValidKey-System-String,System-String,System-String,System-String,System-String-'></a>
### ValidKey() `method`

Checks if inputted key is valid

##### Returns

True if valid, false if invalid.

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-ValidateSegment-System-String-'></a>
### ValidateSegment(segment) `method`

Checks if a segment valid.

##### Returns

True if valid, false if invalid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| segment | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The segment to interpret. |

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-VerifyKey-System-String,System-String,System-String,System-String,System-String-'></a>
### VerifyKey() `method`

Checks if a key is valid as well as all of its segments.

##### Returns

True if the key is valid; false otherwise.

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-VerifyKey-System-String-'></a>
### VerifyKey(key) `method`

Verifies a given key.

##### Returns

TRUE if the key is valid. False, otherwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key to verify. |

