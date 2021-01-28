<a name='assembly'></a>
# BarbezDotEu.License.Generation

## Contents

- [KeyGenerator](#T-BarbezDotEu-License-Generation-KeyGenerator 'BarbezDotEu.License.Generation.KeyGenerator')
  - [#ctor(resultingSum,divider)](#M-BarbezDotEu-License-Generation-KeyGenerator-#ctor-System-Int32,System-String- 'BarbezDotEu.License.Generation.KeyGenerator.#ctor(System.Int32,System.String)')
  - [EXCEPTION](#F-BarbezDotEu-License-Generation-KeyGenerator-EXCEPTION 'BarbezDotEu.License.Generation.KeyGenerator.EXCEPTION')
  - [GenerateKeys(numberOfKeys,excludedKeys)](#M-BarbezDotEu-License-Generation-KeyGenerator-GenerateKeys-System-UInt32,System-String[]- 'BarbezDotEu.License.Generation.KeyGenerator.GenerateKeys(System.UInt32,System.String[])')

<a name='T-BarbezDotEu-License-Generation-KeyGenerator'></a>
## KeyGenerator `type`

##### Namespace

BarbezDotEu.License.Generation

##### Summary

Basic key generator class.

<a name='M-BarbezDotEu-License-Generation-KeyGenerator-#ctor-System-Int32,System-String-'></a>
### #ctor(resultingSum,divider) `constructor`

##### Summary

Constructs a new [KeyGenerator](#T-BarbezDotEu-License-Generation-KeyGenerator 'BarbezDotEu.License.Generation.KeyGenerator').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resultingSum | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The expected "sum" of the key. All keys share the same "sum", which is what makes the key validatable. Negative or default values are not valid. |
| divider | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The desored division in between segments of the license key. |

<a name='F-BarbezDotEu-License-Generation-KeyGenerator-EXCEPTION'></a>
### EXCEPTION `constants`

##### Summary

One or more parameters are invalid. NULL, negative, empty or default values are not valid parameters.

<a name='M-BarbezDotEu-License-Generation-KeyGenerator-GenerateKeys-System-UInt32,System-String[]-'></a>
### GenerateKeys(numberOfKeys,excludedKeys) `method`

##### Summary

Generates a number of license keys.

##### Returns

The generated license keys.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| numberOfKeys | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | The amount of keys to generate. |
| excludedKeys | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Keys that cannot be present in the resulting key set. |
