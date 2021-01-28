<a name='assembly'></a>
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
