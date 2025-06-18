# Space Shooter!

# Core

Berisi semua script untuk mendefinisikan data seperti interface, abstract class, base class, dan utils.

## Bullet

### IBulletConfig

Interface untuk builder method.

## Pool

### Pool\<T\>

Base class untuk membuat pool. Dibuat dengan generics untuk mempermudah pemanggilan pool. Pool tinggal menjadi subclass kelas ini dengan script objectnya. (cth: Pool\<Bullet\>)

### IPoolable

Marking interface untuk memastikan tiap tiap item yang dapat di pool memiliki setup dan reset. Setup sendiri memiliki signature (Action onRelease) agar item tersebut yang me-release dirinya sendiri, berguna untuk bullet dan item.

# Utils

## Entity Type

Enum untuk mempermudah mendapatkan layer mask. Kelas enum ini memiliki static extension untuk mendapatkan LayerMask object tersebut atau LayerMask yang dapat mendamage objek tersebut. (GetMask(), GetDamagingMask())

## MinMax & MinMaxFloat

Kelas untuk memanggil randomized value dari min dan max.

## IReactive

Base class untuk Reactive utils. Dibuat untuk memastikan enkapsulasi seperti memastikan script luar tidak bisa *raise event* atau mengubah value.

## ReactiveSubject & ReactiveSubjet\<T\>

Wrapper Action untuk events. Daripada subscribe dengan “+=” kita tinggal menggunakan .Subscribe(Action callback). Kelas *raise event* secara manual dengan .Raise() atau .Raise(T value)

## ReactiveProperty\<T\>

Wrapper ReactiveSubject\<T\> namun dengan value dimana value tersebut akan *raise event* ketika di ubah dengan value baru. Simple dengan set get property accessor.

## SubscriptionBag

Script yang mempermudah hidup. When subscribing with any reactive class it returns an IDisposeable. All this class do is store those IDisposeable in a list and when we need to unsubscribe we can just .Dispose() the bag. There is also a static extension to IDisposeable that is the primary way to add subscriptions that we could just .AddTo(\_bag) it.

# Commons

### Sound System

Sebuah singleton untuk mengeluarkan suara.

# Feature

## Bullet

Bullet terdiri dari tiga bagian: BulletPool, BulletBehavior, BulletPattern. 

### BulletPool

Berisi *pool prefabs* dari script Bullet. Dari sini semua sisi dapat me-*request*

## Enemy

Enemy terdiri dari beberapa bagian: EnemyHealth, BehaviorActor, dan Behavior itu sendiri.

### Enemy Health

Dibedakan dari player health untuk mempermudah Subscribing ke OnDeath event;

### Behavior Actor

Sebuah base class dimana kelas tersebut berisi state machine. State tersebut antara lain:  
Idle, Hostile, Alert. Idle adalah state pada awalnya, Hostile saat player memasuki trigger dari enemy tersebut, Alert adalah beberapa waktu dari saat player keluar trigger. EnemyBehavior akan di run sesuai dari state tersebut. Untuk Menggunakannya tinggal di *subclass* lalu di Awake() susun behavior sesuai keinginan

### Enemy Behavior

Enemy behavior merupakan *abstract* *class* dimana berisi semua *unity message* seperti OnEnter(), OnUpdate(), OnCollisionEnter2d(), dll. Subclass dari EnemyBehavior dapat berupa Behavior, Act, dan Modifier.

#### Behavior

Behavior merupakan *wrapper* seperti ParalelBehavior dimana akan menjalankan banyak EnemyBehavior yang lain secara paralel. Ada juga RandomBehavior, NullBehavior, dll.

#### Act

Act merupakan *micro-behavior* berisi satu tujuan. Acts akan di susun di dalam ParallelBehavior atau yang lainnya. Beberapa Act ada ChargeToDir, RotateToDir, ShootToDir.

#### Modifier

Modifier mengubah variabel yang ada di dalam Behavior Actor atau memodifikasi Act. Seperti DirToPlayerModifier, ThrottleModifier, ConditionalModifier.

## Item

Itemscript terdiri dari beberapa komponen, kita mulai dari yang paling dasar *‘building blocks’* dari item script adalah ItemData(dot)cs script ini adalah dasar dari setiap SO (scriptable object), mulai dari HP, XP, Shield, dan BulletItemData. Mereka mengambil template dari ItemData dan *inherit* beberapa hal.

Lalu masing masing item tersebut memiliki pool nya, dan d

## Player

## UI

Pembuatan UI System menggunakan UI Builder untuk desain. UI system terdiri atas:

* Player Level  
* Player health  
* Current Bullet type  
* Menu screen  
* Pause screen

# Assembly

## Building the Player

Game object Player berisi:

- Rigidbody2D untuk collision  
- Rigidbody2D untuk trigger  
- CapsuleCollider2D sebagai collider utama tubuh pemain  
- SpriteRenderer untuk menampilkan visual karakter pemain  
- Animator untuk mengatur animasi pemain seperti berjalan, menyerang, atau idle  
- AudioSource untuk memutar suara efek seperti langkah kaki atau serangan  
- BehaviorPlayer (script turunan dari BehaviorActor) untuk mengatur logika utama pemain, termasuk input movement, attacking, dan interaksi dengan dunia  
- HealthSystem (script) untuk mengelola jumlah nyawa dan kerusakan yang diterima  
- WeaponHandler untuk menangani senjata yang digunakan pemain

## Building the Enemy

Game object itu sendiri merupakan prefab. Ini memungkinkan untuk membuat banyak configurasi musuh dan memberikan kontrol kepada desainer level. Game Object terdiri dari:

- CircleCollider2d dengan mode trigger untuk deteksi player  
- CircleCollider2d untuk menjadi hitbox tubuh musuh  
- BehaviorActor (derived script) ini adalah script yang merupakan anak dari BehaviorActor. Dimana behavior enemy ini dibuat.  
- SpriteRenderer untuk gambar dari musuh itu sendiri

## Building the Level

Level terdiri dari 5, masing-masing memiliki kesulitan, rintangan, jumlah dan jenis musuh, dan layout yang berbeda. Setiap level terdiri dari *waves* dengan jumlah yang berbeda dengan menggunakan Script EnemyStage, dimana ketika musuh di satu *wave* sudah kalah akan segera lanjut ke *wave* berikutnya dengan memunculkan musuh baru. Namun jika setiap *waves* sudah ditamatkan, maka akan lanjut ke level berikutnya.
