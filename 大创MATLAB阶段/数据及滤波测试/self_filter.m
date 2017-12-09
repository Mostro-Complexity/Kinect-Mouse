function self_filter(path)
%% smooth��������
%'moving'������ν��ƽ���˲����򵥵�ƽ������
%'lowess'һ�׶���ʽ��Ȩ���Իع�
%'loess'һ�׶���ʽ��Ȩ���Իع�
%'rlowess'ͬ'lowess'����������Ӧ����Ȩֵ
%'rloess'ͬ'loess'����������Ӧ����Ȩֵ
%% smooth����
src=load(path);
src=src(:,9:10);

figure
plot(src(:,1),src(:,2));
hold on
plot(smooth(src(:,1),'moving'),smooth(src(:,2),'moving'),'-');
hold on
plot(smooth(src(:,1),'lowess'),smooth(src(:,2),'lowess'),'-');
hold on
plot(smooth(src(:,1),'loess'),smooth(src(:,2),'loess'),'-');
hold on
plot(smooth(src(:,1),'rlowess'),smooth(src(:,2),'rlowess'));
hold on
plot(smooth(src(:,1),'rloess'),smooth(src(:,2),'rloess'));
hold off
legend('Դ����','smooth.moving','smooth.lowess','smooth.loess',...
    'smooth.rlowess','smooth.rloess');

%% medfilt��ֵ�˲�
figure
plot(src(:,1),src(:,2));
hold on
plot(medfilt1(src(:,1),30),medfilt1(src(:,2),30))
hold off
legend('Դ����','medfilt1','medfilt2');
end