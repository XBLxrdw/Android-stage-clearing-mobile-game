# 悦动大逃亡 · Snowball Escape

![悦动大逃亡](Assets/04_Art/美术资源/logo/跃动大逃亡.png)

一款基于 **Unity 6 + URP 2D** 开发的雪地滑行闯关手游：操控一团蓝色雪球史莱姆沿着雪山斜坡一路下滑，左右切换滑行方向，躲避岩石陷阱与拦路史莱姆，冲过终点完成关卡。

![Unity](https://img.shields.io/badge/Unity-6000.0.28f1c1-000000?logo=unity&logoColor=white)
![URP](https://img.shields.io/badge/URP-17.0.3-2C8EBB)
![Platform](https://img.shields.io/badge/Platform-Android-3DDC84?logo=android&logoColor=white)
![Input](https://img.shields.io/badge/Input%20System-1.11.2-5C2D91)
![License](https://img.shields.io/badge/License-未指定-lightgrey)

---

## 🎮 玩法

- 角色默认沿 **-50° 方向**匀速下滑，摄像机只跟随角色的 Y 轴，营造"越滑越深、越滑越快"的节奏感。
- **一次点击 = 一次转向**：按下时角色朝 -40° 偏转并翻转朝向，松开后回正到 50° 方向，靠"点按时机"在障碍之间穿行。
- 按下转向的同时速度会略微下降（-2），松开后逐渐恢复（+2），形成"贴边擦身"的操作博弈。
- 场景中包含 `Slime`（史莱姆障碍）、`Trap`（岩石陷阱）与 `End`（终点）三类交互对象，撞上障碍即失败，抵达终点即过关。
- 关卡 UI 已就位：关卡编号、当前关卡、下一关卡、教程提示、设置/音乐开关、积分与结算（finish / yeah / confetti）等素材均已完成导入。

### 操作

| 平台 | 操作 | 说明 |
| --- | --- | --- |
| Android / 触屏 | 点击屏幕 | 转向（按住持续偏转，松开回正） |
| PC / 编辑器 | `Space` 空格 | 同上，便于在编辑器内调试 |

## ✅ 当前进度

已完成：

- **玩家移动与转向**：基于 `Rigidbody2D.linearVelocity` 的匀速滑行 + 方向插值转向，附带加速/减速手感。
- **状态机框架**：`IState` / `StateMachine` / `PlayerState` / `PlayerStateMachine` 四层结构，状态以 `ScriptableObject` 资产形式配置（已实现 `PlayerState_Idle`），支持后续扩展 Turn / Hit / Dead / Finish 等状态。
- **数据分层**：`Player_Data`（运行时可变数据）与 `Player_Static_Data`（可配置静态数据资产）分离。
- **输入系统**：Unity Input System 生成的 `Player_cont` 包装类，触屏与键盘共用同一套 Action。
- **摄像机系统**：仅跟随 Y 轴的 2D 跟随摄像机。
- **美术 / 音频 / UI 资源**：角色动画序列帧、雪山场景、史莱姆 UI 套件、6 段音效（ski / dead / fever / levelUp / music / perfect）与中文字体（SDF）均已导入工程。

开发中：

- 关卡流程与结算逻辑（失败判定、过关弹窗、关卡推进）
- 障碍物生成与难度曲线
- 计分（积分）系统与音效播放时机
- Android 打包与真机适配

## 🧱 技术栈

| 项 | 选型 |
| --- | --- |
| 引擎 | Unity **6000.0.28f1c1**（Unity 6） |
| 渲染 | Universal Render Pipeline **17.0.3**（2D Renderer + Global Light 2D） |
| 输入 | com.unity.inputsystem **1.11.2** |
| UI | uGUI 2.0.0 + TextMesh Pro |
| 动画 | 2D Sprite 序列帧 + Animator Controller |
| 目标平台 | Android（横屏，默认 1920×1080） |

## 📁 目录结构

```
悦动大逃亡/
├─ Assets/
│  ├─ 00_Scenes/          # 主场景 SampleScene.unity
│  ├─ 01_Settings/        # URP 资产、Renderer2D、场景模板
│  ├─ 02_Scripts/         # 代码（Input / Players / Camera）
│  ├─ 03_Prefabs/         # Player / Slime / Trap / End 预制体
│  ├─ 04_Art/             # 美术资源（角色动画、场景、UI、logo、音乐音效、字体）
│  ├─ 05_Data/            # 数据资产（动画控制器、材质、PlayerData、状态资产）
│  └─ 06_TileMap/         # Tilemap 资源预留
├─ Packages/              # 包依赖清单 manifest.json
├─ ProjectSettings/       # 工程与打包设置
├─ .gitignore             # Unity 官方模板（忽略 Library/Logs/obj/.vs/*.csproj 等）
└─ 悦动大逃亡.sln          # 由 Unity 自动生成的解决方案（未纳入版本管理）
```

> `Library/`、`Logs/`、`obj/`、`UserSettings/`、`.vs/` 以及自动生成的 `*.csproj`、`*.sln` 均已加入 `.gitignore`，克隆后由 Unity 自行重建。

## 🚀 打开与运行

1. 安装 **Unity 6000.0.28f1c1**（可通过 Unity Hub 添加该版本，或使用兼容的 Unity 6 LTS 版本）。
2. 克隆仓库后用 Unity Hub 添加本目录并打开工程（首次打开会重建 `Library/`，耗时取决于机器性能）。
3. 打开场景 `Assets/00_Scenes/SampleScene.unity`，点击 Play 即可试玩；编辑器内用 `Space` 代替触屏点击。
4. Android 打包：`File > Build Settings` 切换到 Android 平台，配置好 SDK/NDK 后 Build 或 Build And Run。

```bash
git clone https://github.com/XBLxrdw/Android-stage-clearing-mobile-game.git
```

## 🧩 代码架构简述

状态机是本工程的核心骨架，所有玩家行为都拆分为可插拔的状态资产：

```csharp
public interface IState
{
    void Enter();          // 进入状态
    void Exit();           // 离开状态
    void LogicUpdate();    // 逻辑帧（Update）
    void PhysicsUpdate();  // 物理帧（FixedUpdate）
}
```

- `StateMachine`：持有 `Dictionary<Type, IState>` 状态表，统一调度 `Update` / `FixedUpdate` 并负责状态切换。
- `PlayerState`：继承 `ScriptableObject` 的玩家状态基类，注入 `Animator`、`Player_Data`、`Player_Control_System`，通过 `Animator.CrossFade` 做动画过渡。
- `PlayerStateMachine`：在 `Awake` 中把 Inspector 里配置的状态数组注册进状态表，`Start` 时切入 `PlayerState_Idle`。
- 新增状态只需：新建一个继承 `PlayerState` 的类 → `Create > Data/Player/State/...` 创建资产 → 拖入 `PlayerStateMachine` 的 `states` 数组。

数据层：`Player_Data` 只做"数据仓库"，不写逻辑；`Player_Control_System` 负责输入读取与刚体驱动。逻辑改动集中在状态类里，避免相互耦合。

## 🗺️ Roadmap

- [ ] 补齐 Turn / Hit / Dead / Finish 状态与状态间转换条件
- [ ] 障碍物（Slime / Trap）生成器与关卡难度曲线
- [ ] 结算面板、关卡推进与存档
- [ ] 积分与连击（fever）机制
- [ ] 音效与背景音乐的运行时接入（目前仅导入资源）
- [ ] Android 构建产物与真机性能验证

## 📄 说明

- 本项目为个人练习 / 原型项目，美术与音频素材仅用于学习演示，请勿商用。
- 许可证：**未指定**（如需开源发布，可补充 MIT / Apache-2.0 等协议文件）。
- 仓库地址：<https://github.com/XBLxrdw/Android-stage-clearing-mobile-game>
