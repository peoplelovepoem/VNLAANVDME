# Kiểm tra Tetris

Build Release trước bằng Visual Studio hoặc MSBuild. Chạy từ thư mục gốc repo trong PowerShell:

```powershell
New-Item -ItemType Directory -Path Tests/bin -Force | Out-Null
Copy-Item Tetris/bin/Release/Tetris.exe Tests/bin/Tetris.exe
$csc = "$env:WINDIR/Microsoft.NET/Framework64/v4.0.30319/csc.exe"
& $csc /nologo /out:Tests/bin/WelcomeFlowTests.exe /reference:Tests/bin/Tetris.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /reference:System.Core.dll Tests/WelcomeFlowTests.cs
& ./Tests/bin/WelcomeFlowTests.exe
& $csc /nologo /out:Tests/bin/GameplayFeaturesTests.exe /reference:Tests/bin/Tetris.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll Tests/GameplayFeaturesTests.cs
& ./Tests/bin/GameplayFeaturesTests.exe
& $csc /nologo /out:Tetris/bin/Release/AudioPlaybackTests.exe /reference:Tetris/bin/Release/Tetris.exe /reference:System.Windows.Forms.dll Tests/AudioPlaybackTests.cs
& ./Tetris/bin/Release/AudioPlaybackTests.exe
```

- WelcomeFlowTests: validation, nhớ tên, file hỏng, dữ liệu thành tích cũ/mới, lưu một lần khi thua và chuyển Welcome/Menu.
- GameplayFeaturesTests: khối rơi cổ điển, một khối tiếp theo, bố cục khi đổi kích thước, tạm dừng và các nút trong cài đặt.
- AudioPlaybackTests: thiết bị phát nhạc/hiệu ứng độc lập, đổi nhạc, bật/tắt và âm lượng. Test này phát âm thanh trong thời gian ngắn.

Tests/bin dùng dữ liệu thành tích riêng, không sửa leaderboard của game. Kiểm tra Scale trong WelcomeFlowTests là mô phỏng; vẫn cần xem giao diện ở DPI Windows 100%, 125%, 150% và nghe âm thanh thực tế.

## Các phần mã chính

- Form1.cs: trạng thái ván chơi, điều khiển, tạm dừng và kết thúc ván.
- Form1.Rendering.cs: bố cục và vẽ bảng, khối và Next.
- GameSetting/AudioSettings.cs: trạng thái bật/tắt và âm lượng.
- GameSetting/AudioPlayback.cs: phát nhạc và hiệu ứng bằng MCI.
- Các file *.Designer.cs: control và giao diện chỉnh trong Visual Studio.

Tên được nhớ ở %LocalAppData%/Tetris/player-name.txt. Thành tích ở leaderboard.txt cạnh exe; hỗ trợ cả Tên|Điểm và Tên|Điểm|SốGiây. Ứng dụng chỉ dùng tên hiển thị, không có mật khẩu hoặc SQL.
